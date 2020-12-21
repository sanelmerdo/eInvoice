import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

/** Service used for http communication */
@Injectable({
  providedIn: 'root'
})
export class HttpService {

  apiUrl: string;

  /** Http communication options */
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  /** Service constructor */
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiUrl = baseUrl + 'api/';
  }

  /** Get data from the response */
  private extractData(res: Response) {
    let body = res;
    return body || {};
  }

  /** get full url */
  private getUrl(endPoint) {
    return this.apiUrl + endPoint;
  }

  /**
   * Perform an HTTP get request
   * @param endPoint end point to target
   */
  getRequest<T>(endPoint): Observable<T> {
    return this.http.get(
      this.getUrl(endPoint),
    ).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('getRequest'))
    );
  }

  /**
* Perform an HTTP get request
* @param endPoint end point to target
*/
  getRequestWithResponseType<T>(endPoint, responseTypeName): Observable<T> {
    return this.http.get(
      this.getUrl(endPoint),
      { responseType: responseTypeName }
    ).pipe(
      catchError(this.handleError<any>('getRequest'))
    );
  }

  /**
   * Perform an HTTP post request
   * @param endPoint end point to target
   * @param data data to send on the request
   * @param stringify should data be stringified (default: true)
   */
  postRequest<T>(endPoint, data, stringify = true, observeResponse = false): Observable<T> {

    return this.http.post<any>(
      this.getUrl(endPoint),
      stringify ? JSON.stringify(data) : data,
      !observeResponse
        ? this.httpOptions
        : {
          headers: this.httpOptions.headers,
          observe: "response" as 'body',
          responseType: "json"
        }
    ).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('postRequest'))
    );
  }

  /**
   * Perform and HTTP put request
   * @param endPoint end point to target
   * @param data data to send on the request
   * @param stringify should data be stringified (default: true)
   */
  putRequest<T>(endPoint, data, stringify = true): Observable<T> {
    return this.http.put<any>(
      this.getUrl(endPoint),
      stringify ? JSON.stringify(data) : data,
      this.httpOptions
    ).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('putRequest'))
    );
  }

  /**
   * Perform and HTTP delete request
   * @param endPoint end point to target
   */
  deleteRequest<T>(endPoint): Observable<T> {
    return this.http.delete<any>(
      this.getUrl(endPoint),
      this.httpOptions
    ).pipe(
      map(this.extractData),
      catchError(this.handleError<any>('deleteRequest'))
    );
  }


  /**
   * Function to handle errors
   * @param operation Operation perform when the request failed
   */
  private handleError<T>(operation = ' operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging

      console.error(`Error on ${operation}, failed: ${error.message}`);

      //return of(result as T); // return empty T so operation continues normally
      return throwError(error);
    };
  }
}
