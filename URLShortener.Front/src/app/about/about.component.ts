import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AboutContent } from '../models/AboutContent';
import { AccountModel } from '../models/AccountModel';
import { ResponseModel } from '../models/ResponseModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit{
  public isPreview = false;
  public isEdit = false;
  public algorithmInfo: AboutContent = new AboutContent(0,'');
  public newAlgorithmInfo: AboutContent = new AboutContent(0,'');
  public currentAccount!: AccountModel;
  url: string = environment.API_URL;

  constructor(private http: HttpClient, public accountService: AccountService) {  }

  ngOnInit(){
    this.accountService.currentAccount$.subscribe(x=>{
      this.currentAccount = x as AccountModel;
      this.getContent();
    });
    
  }

  saveContent(){
    this.updateContent();
  }

  editMode(){
    this.isEdit = !this.isEdit;
    this.newAlgorithmInfo.id = this.algorithmInfo.id;
    this.newAlgorithmInfo.content = this.algorithmInfo.content;
    return;
  }

  previewModeWhenEdit(){
    this.isPreview = !this.isPreview;
  }

  updateContent(){
    var headers = this.accountService.makeJWTHeader();
    return this.http.patch<ResponseModel<AboutContent>>(this.url + 'api/AboutContent', this.newAlgorithmInfo, {headers}).subscribe(x=>{
      this.algorithmInfo = x.data;
      this.isEdit = false;
      this.isPreview = false;
    });
  }
  getContent(){
    return this.http.get<ResponseModel<AboutContent>>(this.url + 'api/AboutContent').subscribe(x=>
      {
        this.algorithmInfo = x.data;
      });
  }
}
