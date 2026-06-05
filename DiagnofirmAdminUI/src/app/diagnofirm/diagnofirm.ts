import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { DataService } from './services/data.service';
import { GlobalConstants } from './services/global.constant';
import { NotificationService } from './services/notification.service';
import { CustomerService } from '@/pages/service/customer.service';
import { ProductService } from '@/pages/service/product.service';
import { ConfirmationService, MessageService, FilterService } from 'primeng/api';
import { AppLayout } from '@/layout/component/app.layout';

@Component({
  selector: 'app-diagnofirm',
  imports: [CommonModule, TableModule, ButtonModule, RippleModule, AppLayout],
  templateUrl: './diagnofirm.html',
  styleUrl: './diagnofirm.scss',
   providers: [
    ConfirmationService,
    MessageService,
    CustomerService,
    ProductService,
    NotificationService,
    FilterService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class Diagnofirm implements OnInit {

  allorder: any[] = [];
  allordercount: any = [];

  constructor(
    private dataService: DataService,
    private cdr: ChangeDetectorRef,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.loadcount();
    this.loadorders();
  }

  loadorders() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getallorder;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status === 'success') {
        this.allorder = response['response']['ref1'];

        //this.allordercount = response['response']['ref1'][0];
        this.cdr.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'No order data found');
      }
    });
  }

  loadcount() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getallcount;

    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status === 'success') {
        this.allordercount = response['response']['ref1'][0];
        this.cdr.detectChanges();
      } else {
        this.notificationService.showMessage('error', 'Error', 'No order data found');
      }
    });
  }

  viewOrder(order: any) {
    console.log('View order:', order);
    // 👉 navigate to order detail page or open a dialog
    // this.router.navigate(['/orders', order.checkoutid]);
  }
}