import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        // handle your auth error or rethrow
        if (err.status === 401 || err.status === 403) {
            // navigate /delete cookies or whatever
            this.router.navigateByUrl('/login');
            // if you've caught / handled the error, you don't want to rethrow
            // it unless you also want downstream consumers to have to handle it as well.
            // return of(err); // or EMPTY may be appropriate here
        }
        return throwError(err);
    }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.token; // you probably want to store it in localStorage or something
    if (token === undefined) {
      return next.handle(req).pipe(catchError( x => this.handleAuthError(x)));
    }

    const req1 = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`),
    });

    return next.handle(req1);
  }

}
