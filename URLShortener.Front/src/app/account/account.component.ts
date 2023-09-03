import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountModel } from '../models/AccountModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  public accountUpdateForm!: FormGroup | null;;
  public currentAccount!: AccountModel;
  public ErrorList: string[] = [];
  url: string = environment.API_URL;

  constructor(private router: Router, private formBuilder: FormBuilder, private http: HttpClient, private accountService: AccountService) {  }

  ngOnInit() {
    this.accountUpdateForm = null;
    this.getCurrentAccount();
  }

  getCurrentAccount(){
    return this.accountService.currentAccount$.subscribe(x=>{
      if(x){
        this.currentAccount = x;
        this.currentAccount.jwtToken = '';
      }
    });
  }

  updateAccount(){
    this.initErrorList();
    var formValue = this.accountUpdateForm?.value;
    this.accountService.update(formValue.id,formValue).subscribe(x=>{
      if(x.data){
        this.accountService.saveCurrentAccount(x.data);
          this.router.navigateByUrl('/').then(x => {
            window.location.reload();
          });
      }
      else{
        x.errors.forEach((i) => {
          this.ErrorList.push(i);
        });
      }
    })
  }

  initErrorList(){
    this.ErrorList = [];
  }

  initAccountUpdateform(){
    if(this.accountUpdateForm === null){
      this.accountUpdateForm = this.formBuilder.group({
        id: ['', Validators.required],
        username: ['', Validators.required],
        oldPassword: [''],
        newPassword: [''],
        confirmNewPassword: [''],
      });
      this.accountUpdateForm.controls['id'].setValue(this.currentAccount.id);
      this.accountUpdateForm.controls['username'].setValue(this.currentAccount.username);
    }
    else{
      this.accountUpdateForm = null;
      this.initErrorList();
    }
  }
}
