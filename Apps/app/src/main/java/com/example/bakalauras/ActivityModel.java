package com.example.bakalauras;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;

import com.google.ar.core.Anchor;
import com.google.ar.sceneform.AnchorNode;
import com.google.ar.sceneform.assets.RenderableSource;
import com.google.ar.sceneform.rendering.ModelRenderable;
import com.google.ar.sceneform.ux.ArFragment;

import java.nio.file.Path;

public class ActivityModel extends AppCompatActivity {

    private ArFragment arFragment;
    //private String asset3d = "https://arnpen.blob.core.windows.net/gltfs/Astronaut.glb";
    private String asset3d = "";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_model);

        Intent intent = getIntent();
        asset3d = intent.getStringExtra("fileUrl");
        arFragment = (ArFragment) getSupportFragmentManager().findFragmentById(R.id.arFragment);

        arFragment.setOnTapArPlaneListener((hitResult, plane, motionEvent) -> {
            placeModel(hitResult.createAnchor());
        });
    }
    public ActivityModel(){

    }

    private void placeModel(Anchor anchor) {
        ModelRenderable
                .builder()
                .setSource(this,
                        RenderableSource
                        .builder()
                        .setSource(this, Uri.parse(asset3d), RenderableSource.SourceType.GLB)
                        .setScale(0.75f)
                        .setRecenterMode(RenderableSource.RecenterMode.ROOT)
                        .build()
                ).setRegistryId(asset3d)
                .build()
                .thenAccept(modelRenderable -> addNodeToScene(modelRenderable,anchor))
                .exceptionally(throwable -> {
                    AlertDialog.Builder builder  = new AlertDialog.Builder(this);
                    builder.setMessage(throwable.getMessage()).show();
                    return null;
                });
    }

    private void addNodeToScene(ModelRenderable modelRenderable, Anchor anchor) {
        AnchorNode anchorNode = new AnchorNode(anchor);
        anchorNode.setRenderable(modelRenderable);
        arFragment.getArSceneView().getScene().addChild(anchorNode);
    }
}
