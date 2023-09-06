import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-modals',
  templateUrl: './modals.component.html',
  styleUrls: ['./modals.component.css']
})
export class ModalsComponent {
  currentModal!: BsModalRef;

  constructor(private modalService: BsModalService) { }

  openModal(modalTitle: string, modalContent: string[], closeBtnName: string){
    const initialState: ModalOptions = {
      initialState: {
        list: modalContent,
        title: modalTitle
      }
    };
    this.initModal(initialState);
    this.currentModal.content.closeBtnName = closeBtnName;
  }
    initModal(initialState: ModalOptions){
      this.currentModal = this.modalService.show(ModalContentComponent,initialState);
  }
}

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'modal-content',
  template: `
    <div class="modal-header">
      <h4 class="modal-title pull-left">{{title}}</h4>
      <button type="button" class="btn-close close pull-right" aria-label="Close" (click)="bsModalRef.hide()">
        <span aria-hidden="true" class="visually-hidden">&times;</span>
      </button>
    </div>
    <div class="modal-body">
      <ul>
        <li *ngFor="let item of list">{{item}}</li>
      </ul>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-default" (click)="bsModalRef.hide()">{{closeBtnName}}</button>
    </div>
  `
})

export class ModalContentComponent {
  title?: string;
  closeBtnName?: string;
  list: string[] = [];
 
  constructor(public bsModalRef: BsModalRef) {}
}
