import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import * as $ from 'jquery';
import { AccountModel } from '../models/AccountModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  currentAccount!: AccountModel;

  constructor(private router: Router, public accountService: AccountService) {
  }
  
  ngOnInit(): void {
    this.accountService.currentAccount$
      .subscribe(x => this.currentAccount = x as AccountModel);
  }

  public isExpanded = false;

  SignOut() {
    this.setDefaultNavbar();
    this.accountService.signOut();
    this.router.navigateByUrl('/').then(() => window.location.reload());
  }
  showHalfNavbar() {
    var elem = $('#navbarSide');
    elem.removeClass('navbar-side-full');
    elem.addClass('navbar-side-half');

    var tips = $('.link-tip-full');
    tips.removeClass('link-tip-full');
    tips.addClass('link-tip-full-hidden');
  }

  showFullNavbar() {
    var elem = $('#navbarSide');
    elem.removeClass('navbar-side-half');
    elem.addClass('navbar-side-full');

    var tips = $('.link-tip-full-hidden');
    tips.removeClass('link-tip-full-hidden');
    tips.addClass('link-tip-full');
  }
  toggle() {
    this.isExpanded = !this.isExpanded;

    if (this.isExpanded === true) {
      this.showFullNavbar();
    }
    else if (this.isExpanded === false) {
      this.showHalfNavbar();
    }
  }
  setDefaultNavbar() {
    var elem = $('#navbarSide');
    this.isExpanded = false;
    if (elem.hasClass('navbar-side-full'))
      this.showHalfNavbar();
  }
}
