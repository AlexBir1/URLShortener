import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AboutContent } from '../models/AboutContent';
import { AccountModel } from '../models/AccountModel';
import { AboutService } from '../services/about.service';
import { AccountService } from '../services/account.service';
import { BaseResponse } from '../services/BaseResponse/BaseResponse';

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

  constructor(private http: HttpClient, public accountService: AccountService, private aboutService: AboutService) {  }

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
    this.isPreview = false;
    this.newAlgorithmInfo.id = this.algorithmInfo.id;
    this.newAlgorithmInfo.content = this.algorithmInfo.content;
    return;
  }

  previewModeWhenEdit(){
    this.isPreview = !this.isPreview;
  }

  updateContent(){
    return this.aboutService.updateAboutPageContent(this.newAlgorithmInfo).subscribe(x=>{
      this.algorithmInfo = x.data;
      this.isEdit = false;
      this.isPreview = false;
    });
  }
  getContent(){
    return this.aboutService.getAboutPageContent().subscribe(x=>
      {
        this.algorithmInfo = x.data;
      });
  }
}
