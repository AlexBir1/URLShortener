import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ShortURLModel } from '../app/models/ShortURLModel';
import { AccountService } from './services/account.service';
import { environment } from 'src/environments/environment';
import { BaseResponse } from './services/BaseResponse/BaseResponse';
import { RedirectService } from './services/redirect.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  private BaseUrl: string = environment.API_URL;
  constructor(private http: HttpClient, private accountService: AccountService, private redirectService: RedirectService){
  }
  ngOnInit(): void {
    var pathname = window.location.pathname;
    var correctPath = pathname.replace('/','');
    this.redirectService.tryRedirectToShortenedURL(correctPath).subscribe(x=>{
      if(x.data){
        window.location.href = x.data;
      }
      else
        window.location.href = environment.APP_URL;
    })
      this.accountService.refreshCurrentAccount();
  }
  title = 'URLShortener';
}
