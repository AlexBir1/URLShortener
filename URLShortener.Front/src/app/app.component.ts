import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ShortURLModel } from '../app/models/ShortURLModel';
import { AccountService } from './services/account.service';
import { environment } from 'src/environments/environment';
import { BaseResponse } from './services/BaseResponse/BaseResponse';
import { RedirectService } from './services/redirect.service';
import { SettingService } from './services/setting.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent{
  private BaseUrl: string = environment.API_URL;
  constructor(private http: HttpClient, private accountService: AccountService, private redirectService: RedirectService, private router: Router, private settingsService: SettingService){
    var pathname = window.location.pathname;
    var correctPath = pathname.replace('/','');
    
    this.redirectService.tryRedirectToShortenedURL(correctPath).subscribe(x=>{
      if(x.data){
        $('app-navbar').hide();
        this.router.navigateByUrl('/redirect');
        window.location.href = x.data;
      }
      else
        window.location.href = environment.APP_URL;
    });
      
      this.accountService.refreshCurrentAccount();
      this.accountService.currentAccount$.subscribe(x => {
        if(x){
          this.settingsService.refreshSettings(x.id);
        }
        else
          this.settingsService.refreshSettings(null);

        this.setSiteTheme();
      });
  }
  title = 'URLShortener';

  setSiteTheme() {
    this.settingsService.currentSettings$.subscribe(x => {
      if (x?.find(x => x.key === 'darkMode')?.isActive) {
        $('body').removeAttr('data-bs-theme');
        $('body').attr('data-bs-theme', 'dark');
      }
      else {
        $('body').removeAttr('data-bs-theme');
        $('body').attr('data-bs-theme', 'light');
      }
    });
  }
}
