import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountModel } from '../models/AccountModel';
import { ShortURLInfoModel } from '../models/ShortURLInfoModel';
import { ShortURLModel } from '../models/ShortURLModel';
import { AccountService } from '../services/account.service';
import { ShortURLService } from '../services/shortURL.service';

@Component({
  selector: 'app-short-url',
  templateUrl: './short-url.component.html',
  styleUrls: ['./short-url.component.css'],
})
export class ShortUrlComponent implements OnInit{
  public ErrorList: string[] = [];
  public shortenedURL: string = '';
  public originURL: string = '';
  public currentAccount!: AccountModel;
  public page: number = 0;
  public urls: ShortURLModel[] = [];
  public selectedUrl: ShortURLInfoModel | undefined = undefined;
  url: string = environment.API_URL;
  constructor(private http: HttpClient, public accountService: AccountService, private shortURLService: ShortURLService, private router: Router){}

  ngOnInit(): void {
    this.accountService.currentAccount$.subscribe(x=>this.currentAccount = x as AccountModel);
    this.getURLs();
  }

  changeCurrentPage(e: any) {
    this.page = e;
  }

  showSelectedURL(id: number){
    if(id === 0)
      return this.selectedUrl = undefined;
    return this.shortURLService.getDetailed(id).subscribe(x=>{
      if(x.data){
        this.selectedUrl = x.data;
      }
      else{
        this.ErrorList = x.errors;
      }
    }); 
  }
  getURLs(){
    return this.shortURLService.getAll().subscribe(x=>{
      if(x.data){
        this.urls = x.data;
      }
      else{
        this.ErrorList = x.errors;
      }
    }); 
  }
  deleteUrl(id: number){
    return this.shortURLService.delete(id).subscribe(x=>{
      if(x.data){
        var index = this.urls.findIndex(t=>t.id === x.data.id);
        this.urls.splice(index,1);
        this.selectedUrl = undefined;
      }
      else{
        this.ErrorList = x.errors;
      }
    }); 
  }

  shortenURL(){
    this.ErrorList = [];
    var newUrl = new ShortURLModel(0,this.originURL, this.originURL, this.currentAccount.username,this.currentAccount.id);
    return this.shortURLService.insert(newUrl).subscribe(x=>{
      if(x.data){
        this.shortenedURL = x.data.url;
        this.urls.push(x.data);
        this.originURL = '';
      }
      else{
        this.ErrorList = x.errors;
      }
    });
  }
}
