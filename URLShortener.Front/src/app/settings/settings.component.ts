import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ModalsComponent } from '../modals/modals.component';
import { SettingModel } from '../models/SettingModel';
import { AccountService } from '../services/account.service';
import { SettingService } from '../services/setting.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit{
  public currentSettings: SettingModel[] = [];
  public currentAccountId: string = '';
  modalComp: ModalsComponent;
  constructor(private settingsService: SettingService, private accountService: AccountService, modalService: BsModalService) {  
    this.modalComp = new ModalsComponent(modalService);
  }
  ngOnInit(): void {
    this.accountService.currentAccount$.subscribe(x=>{
      if(x){
        this.currentAccountId = x.id;
        this.getAccountSettings(x.id);
        
      }
    });
  }

  updateAccountSettings(accountId: string){
    return this.settingsService.updateAccountSetting(accountId, this.currentSettings).subscribe(x=>{
      if(x.data){
        this.currentSettings = x.data;
        this.settingsService.saveCurrentSettings(x.data);
      }
    });
  }

  getAccountSettings(accountId: string){
    return this.settingsService.getAccountSettings(accountId).subscribe(x=>{
        this.currentSettings = x.data;
        this.ensureAccountHasSettings(accountId);
    });
  }

  ensureAccountHasSettings(accountId: string){
    return this.settingsService.getAll().subscribe(x=>{
      if(x.data){
        if(x.data.length != this.currentSettings.length){
          var newCurrentSettings: SettingModel[] = [];
          x.data.forEach(s=>{
            if(!this.currentSettings.find(x=>x.key === s.key)){
              var setting = new SettingModel(s.id, accountId, s.key, s.title, s.description, false);
              newCurrentSettings.push(setting);
            }
          });
          this.settingsService.addAccountSettings(accountId,newCurrentSettings).subscribe(t=>{
            if(t.data){
              this.currentSettings = t.data;
              var setingsDescs: string[] = [];
              newCurrentSettings.forEach(x=>setingsDescs.push(x.description));
              this.modalComp.openModal('New settings', setingsDescs, 'OK!');
            }
          });
        }
      }
    });
  }
}
