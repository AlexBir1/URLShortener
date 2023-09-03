import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AboutContent } from "../models/AboutContent";
import { AccountService } from "./account.service";
import { BaseResponse } from "./BaseResponse/BaseResponse";

@Injectable()
export class AboutService{
    url: string = environment.API_URL;
    constructor(private http: HttpClient, private accountService: AccountService) {
    }
    getAboutPageContent(): Observable<BaseResponse<AboutContent>>{
        return this.http.get<BaseResponse<AboutContent>>(this.url + 'api/AboutContent');
    }
    updateAboutPageContent(newContent: AboutContent): Observable<BaseResponse<AboutContent>>{
        var headers = this.accountService.makeJWTHeader();
        return this.http.patch<BaseResponse<AboutContent>>(this.url + 'api/AboutContent', newContent, {headers})
    }
}