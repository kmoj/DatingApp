import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from "@angular/common/http";
import { catchError } from 'rxjs/operators';
import { error } from 'util';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterCeptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError(Error => {
            if (Error.status === 401) {
                return throwError(Error.statusText);
            }

            if (Error instanceof HttpErrorResponse) {
                // 500 Errors
                const applicationError = Error.headers.get('Application-Error');
                if (applicationError) {
                    return throwError(applicationError);
                }

                // Validate Error
                const serverError = Error.error;
                let modalSateErrors = '';
                if (serverError.errors && typeof serverError.errors === 'object') {
                    for (const key in serverError.errors) {
                        if (serverError.errors[key]) {
                            modalSateErrors += serverError.errors[key] + '\n';
                        }
                    }
                }
                // The Error is not known throw 'Server Error'
                return throwError(modalSateErrors || serverError || 'Server Error');


            }

        })
    );
  }
}

export const ErrorInterCeptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterCeptor,
    multi: true
};
