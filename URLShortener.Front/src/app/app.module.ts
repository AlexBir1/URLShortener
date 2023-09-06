import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ShortUrlComponent } from './short-url/short-url.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { AccountService } from './services/account.service';
import { AuthComponent } from './auth/auth.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountComponent } from './account/account.component';
import { ShortURLService } from './services/shortURL.service';
import { AboutService } from './services/about.service';
import { RedirectService } from './services/redirect.service';
import { RedirectComponent } from './redirect/redirect.component';
import { SettingsComponent } from './settings/settings.component';
import { SettingService } from './services/setting.service';
import { ModalContentComponent, ModalsComponent } from './modals/modals.component';
import { BsModalService, BsModalRef, ModalOptions, ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [
    AppComponent,
    ShortUrlComponent,
    NavbarComponent,
    HomeComponent,
    AboutComponent,
    AuthComponent,
    AccountComponent,
    RedirectComponent,
    SettingsComponent,
    ModalContentComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    ModalModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'about', component: AboutComponent },
      { path: 'auth', component: AuthComponent },
      { path: 'shortUrls', component: ShortUrlComponent },
      { path: 'account', component: AccountComponent },
      { path: 'redirect', component: RedirectComponent },
      { path: 'settings', component: SettingsComponent },
    ]),
  ],
  providers: [AccountService, ShortURLService, AboutService, RedirectService, SettingService, BsModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
