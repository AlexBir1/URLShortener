import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { BaseResponse } from "./BaseResponse/BaseResponse";

@Injectable()
export class RedirectService{
    url: string = environment.API_URL;
    constructor(private http: HttpClient) {
    }
    tryRedirectToShortenedURL(path: string): Observable<BaseResponse<string>>{
        return this.http.get<BaseResponse<string>>(this.url + 'api/ShortURL/TryRedirect/' + path);
    }
}