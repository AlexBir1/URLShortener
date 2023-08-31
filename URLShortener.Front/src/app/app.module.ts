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

@NgModule({
  declarations: [
    AppComponent,
    ShortUrlComponent,
    NavbarComponent,
    HomeComponent,
    AboutComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'about', component: AboutComponent },
      { path: 'auth', component: AuthComponent},
      {path: 'shortUrls', component: ShortUrlComponent}
    ]),
  ],
  providers: [AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
