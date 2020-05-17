import { AfterViewInit, Component, ElementRef, Input, ViewChild, HostListener } from '@angular/core';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';

@Component({
  selector: 'app-scene',
  templateUrl: './scene.component.html',
  styleUrls: ['./scene.component.scss']
})
export class SceneComponent implements AfterViewInit {

  private renderer: THREE.WebGLRenderer;
  private camera: THREE.PerspectiveCamera;
  public scene: THREE.Scene;
  public fieldOfView: number = 40;
  public nearClippingPane: number = 1;
  public farClippingPane: number = 1100;


    


  @ViewChild('canvas')
  private canvasRef: ElementRef;

  constructor() {
    this.render = this.render.bind(this);
    this.onModelLoadingCompleted = this.onModelLoadingCompleted.bind(this);
  }

  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }

  private createScene() {
    this.scene = new THREE.Scene();
    this.renderer = new THREE.WebGLRenderer({
      canvas: this.canvas,
      antialias: true,
      alpha: true
    });
    //cia implementint modeli
    var geometry = new THREE.BoxGeometry(10, 10, 10);
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var cube = new THREE.Mesh(geometry, material);
    this.scene.add(cube);
  }

  private onModelLoadingCompleted(collada) {
    var modelScene = collada.scene;
    this.scene.add(modelScene);
    this.render();
  }

  private createLight() {
    var light = new THREE.PointLight(0xffffff, 1, 1000);
    light.position.set(0, 0, 100);
    this.scene.add(light);

    var light = new THREE.PointLight(0xffffff, 1, 1000);
    light.position.set(0, 0, -100);
    this.scene.add(light);
  }

  private createCamera() {
    let aspectRatio = this.getAspectRatio();
    this.camera = new THREE.PerspectiveCamera(
      this.fieldOfView,
      aspectRatio,
      this.nearClippingPane,
      this.farClippingPane
    );

    // Set position and look at
    this.camera.position.x = 0;
    this.camera.position.y = 0;
    this.camera.position.z = 40;
  }

  private getAspectRatio(): number {
    let height = this.canvas.clientHeight;
    if (height === 0) {
      return 0;
    }
    return this.canvas.clientWidth / this.canvas.clientHeight;
  }

  private startRendering() {
    
    this.renderer.setPixelRatio(devicePixelRatio);
    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);
    this.renderer.shadowMap.enabled = true;
    this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;

    let component: SceneComponent = this;

    (function render() {
      //requestAnimationFrame(render);
      component.render();
    }());
  }

  public render() {
    this.renderer.render(this.scene, this.camera);
  }


  /* EVENTS */

  public onMouseDown(event: MouseEvent) {
    console.log("onMouseDown");
    event.preventDefault();

    // Example of mesh selection/pick:
    var raycaster = new THREE.Raycaster();
    var mouse = new THREE.Vector2();
    mouse.x = (event.clientX / this.renderer.domElement.clientWidth) * 2 - 1;
    mouse.y = - (event.clientY / this.renderer.domElement.clientHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, this.camera);

    var obj: THREE.Object3D[] = [];
    this.findAllObjects(obj, this.scene);
    var intersects = raycaster.intersectObjects(obj);
    console.log("Scene has " + obj.length + " objects");
    console.log(intersects.length + " intersected objects found")
    intersects.forEach((i) => {
      console.log(i.object); // do what you want to do with object
    });

  }

  private findAllObjects(pred: THREE.Object3D[], parent: THREE.Object3D) {
    // NOTE: Better to keep separate array of selected objects
    if (parent.children.length > 0) {
      parent.children.forEach((i) => {
        pred.push(i);
        this.findAllObjects(pred, i);
      });
    }
  }

  public onMouseUp(event: MouseEvent) {
    console.log("onMouseUp");
  }


  @HostListener('window:resize', ['$event'])
  public onResize(event: Event) {
    this.canvas.style.width = "100%";
    this.canvas.style.height = "100%";
    this.camera.aspect = this.getAspectRatio();
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);
    this.render();
  }

  @HostListener('document:keypress', ['$event'])
  public onKeyPress(event: KeyboardEvent) {
    console.log("onKeyPress: " + event.key);
  }

  /* LIFECYCLE */
  ngAfterViewInit() {
    this.createScene();
    this.createLight();
    this.createCamera();
    this.startRendering();
  }

}