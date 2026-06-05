import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
 
@Injectable({
  providedIn: 'root',
})
export class NotificationService {
 
  constructor(private messageService: MessageService) {}
 
  showMessage(severity: string, summary: string, detail: string) {
    //console.log('Toast Triggered:', severity, summary, detail);
    this.messageService.add({ severity, summary, detail });
  }
}