import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';

import { Observable, from, of, throwError } from 'rxjs';
import { catchError, switchMap, concatMap } from 'rxjs/operators';
import { tokenStored, TokenStored } from './token.stored';

let getTokenPromise: Promise<TokenStored> = null;

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (getTokenPromise) {
      return from(getTokenPromise).pipe(
        concatMap(() => this.callRequest(req, next))
      );
    }

    if (!tokenStored.accessToken) {
      return this.getToken().pipe(concatMap(() => this.callRequest(req, next)));
    }

    return this.callRequest(req, next);
  }

  callRequest(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const apiReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${tokenStored.accessToken}`,
      },
    });
    return next.handle(apiReq);
  }

  getToken(): Observable<TokenStored> {
    if (!getTokenPromise) {
      getTokenPromise = fetch('/token')
        .then((response) => response.json())
        .then((tokenObj) => {
          if (tokenObj) {
            tokenStored.accessToken = tokenObj.accessToken;
          }
          return tokenObj;
        })
        .finally(() => {
          getTokenPromise = null;
        });
    }
    return from(getTokenPromise);
  }
}
