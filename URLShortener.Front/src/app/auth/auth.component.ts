import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountModel } from '../models/AccountModel';
import { ResponseModel } from '../models/ResponseModel';
import { SignInModel } from '../models/SignInModel';
import { SignUpModel } from '../models/SignUpModel';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit{
  isRegister: boolean = false;
  operation: string = 'Sign in';
  operationBtn: string = 'Sign up';
  loginForm!: FormGroup;
  registerForm!: FormGroup;
  loginModel!: SignInModel;
  registerModel!: SignUpModel;
  http: HttpClient;
  baseUrl: string = environment.API_URL;
  ErrorList: string[] = [];

  constructor(private formBuilder: FormBuilder, http: HttpClient, private router: Router, private accountService: AccountService) {
    this.http = http;
    this.router = router;
  }

  ngOnInit(): void {
    this.setupLoginForm();
  }

  resultInit() {
    this.ErrorList = [];
  }

  onChangeOperation() {
    this.isRegister = !this.isRegister;
    if (this.isRegister) {
      this.setupRegisterForm();
      this.operation = 'Sign up';
      this.operationBtn = 'Sign in';
    }
    else {
      this.setupLoginForm();
      this.operation = 'Sign in';
      this.operationBtn = 'Sign up';
    }
  }

  onLoginSubmit() {
    this.resultInit();
    if (this.loginForm.valid) {
      this.loginModel = this.loginForm.value;
      this.accountService.SignIn(this.loginModel).subscribe((x: ResponseModel<AccountModel>) => {
        if (x.data) {
          this.accountService.saveAccount(x.data);
          this.router.navigateByUrl('/').then(x => {
            window.location.reload();
          });
          
        }
        else {
          x.errors.forEach((i) => {
            this.ErrorList.push(i);
          });
        }
      });
    }
  }

  onRegisterSubmit() {
    this.resultInit();
    if (this.registerForm.valid) {
      this.registerModel = this.registerForm.value;
      this.accountService.SignUp(this.registerModel).subscribe((x: ResponseModel<AccountModel>) =>
      {
        if (x.data) {
          this.accountService.saveAccount(x.data);
          this.router.navigateByUrl('/').then(x => {
            window.location.reload();
          });
        }
        else {
          x.errors.forEach((i) => {
            this.ErrorList.push(i);
          });
        }
      }, (error: HttpErrorResponse) => this.ErrorList = Object.values(error.error.errors));
    }
  }

  setupRegisterForm() {
    this.resultInit();
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required],
    });
  }
  setupLoginForm() {
    this.resultInit();
    this.loginForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  validateFields(form: FormGroup) {
    Object.keys(form.controls).forEach(i => {
      const control = form.get(i);
      if (control instanceof FormControl)
          control.markAsDirty({ onlySelf: true });
      if (control instanceof FormGroup)
        this.validateFields(form);
    })
  }
}
