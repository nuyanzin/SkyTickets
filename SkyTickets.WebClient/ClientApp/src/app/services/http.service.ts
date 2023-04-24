import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { AppConfig } from "../app.config";

@Injectable()
export class HttpService {
    constructor(
        private readonly http: HttpClient,
        private readonly appConfig: AppConfig
    ) {}

    public get<T>(url: string): Observable<any> {
        url = `${this.appConfig.skyTicketsApiUrl}/${url}`
        return this.http.get<T>(url, this.getHttpRequestOptions())
            .pipe(map(response => this.handleResponce(response as HttpResponse<T>)));
    }

    public post<T>(url: string, data: any): Observable<any> {
        url = `${this.appConfig.skyTicketsApiUrl}/${url}`
        let sendData = null;
        if (data instanceof FormData) {
            sendData = data;
        } else {
            sendData = JSON.stringify(data);
        }
        return this.http.post<T>(url, sendData, this.getHttpRequestOptions())
            .pipe(map(response => this.handleResponce(response as HttpResponse<T>)));
    }

    private getHttpRequestOptions(): any {
        const contentType = 'application/json';
        return {
            headers: this.getHttpHeaders(contentType),
            withCredentials: false,
            responseType: null,
            observe: 'response'
        };
    }

    private getHttpHeaders(contentType?: string): HttpHeaders {
        let headers = new HttpHeaders();
        if (contentType && contentType !== 'default') {
            headers = headers.append('content-type', contentType);
        }

        return headers;
    }

    private handleResponce<T>(response: HttpResponse<T>): T | null {
        return response.body;
    }
}