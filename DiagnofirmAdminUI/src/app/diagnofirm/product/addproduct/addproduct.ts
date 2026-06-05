import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { firstValueFrom, take } from 'rxjs';

@Component({
  selector: 'app-addproduct',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    DialogModule,
    FileUploadModule,
    InputTextModule,
    MultiSelectModule,
    SelectModule,
    Imageupload,
    EditorModule
  ],
  templateUrl: './addproduct.html',
  styleUrl: './addproduct.scss'
})
export class Addproduct {

  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded: EventEmitter<any> = new EventEmitter();

  Lastcode: any;
  username!: number;
  packageid!: string;
  imageFileval!: any[];
  imagejsonvalue: any;
  photoinfo: any;
  testdata: any;

  // =========================
  // FORM FIELDS
  // =========================
  category: number | null = null;
  subcategory: any[] = [];

  prod_headname: string = '';
  prod_code: string = '';
  prod_name: string = '';
  prod_desc: string = '';
  prod_ord: string = '';
  prod_price: number | null = null;
  prod_grpcod: string = '';
  is_active: string = '1';

  usr_id: number | null = null;
  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;

  // =========================
  // LISTS
  // =========================
  getcategoryist: any[] = [];
  getsubcategoryist: any[] = [];

  statusList = [
    { label: 'Active',   value: '1' },
    { label: 'Inactive', value: '0' }
  ];

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService,
  ) {}

  // =========================
  // LIFECYCLE
  // =========================
  ngOnInit() {
    this.testdata = true;
    this.getcategory();
    this.getlastcode('diafrm', 'product_tbl', 'prod_code');
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getcategory();
    this.getlastcode('diafrm', 'product_tbl', 'prod_code');
    this.CDR.detectChanges();
  }

  // =========================
  // IMAGE REQUIRED RULE
  // =========================

  /** Image is mandatory only when a product group code is entered */
  isImageRequired(): boolean {
    return !!this.prod_grpcod?.trim();
  }

  /** Called on every keystroke in the group code textarea */
  onGroupCodeChange(): void {
    this.CDR.detectChanges();
  }

  // =========================
  // LAST CODE
  // =========================
  generateNextCode(lastCode: string): string {
    const prefix = 'PROD';
    if (!lastCode) return `${prefix}-0001`;
    const lastNumber = parseInt(lastCode.split('-')[1], 10);
    return `${prefix}-${(lastNumber + 1).toString().padStart(4, '0')}`;
  }

  getlastcode(schemaname: any, tablename: any, columnname: any) {
    const input = { schemaname, tablename, columnname };
    const url = GlobalConstants.Authurl + GlobalConstants.Getlastcode;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        const value = response?.response?.ref1?.[0]?.ref1 ?? 'PROD-0000';
        this.Lastcode = value;
        this.prod_code = this.generateNextCode(this.Lastcode);
      }
    });
  }

  // =========================
  // CATEGORY / SUBCATEGORY
  // =========================
  getcategory() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getcategory;
    this.dataService.getData(url).subscribe((response: any) => {
      if (response.status === 'success') {
        this.getcategoryist = response.response.ref1;
        this.CDR.detectChanges();
      }
    });
  }

  onCategoryChange(event: any) {
    this.subcategory = [];
    this.getsubcategoryist = [];
    this.getsubcategory(event.value);
  }

  getsubcategory(categoryId: any) {
    const input = { categoryid: Number(categoryId) };
    const url = GlobalConstants.Authurl + GlobalConstants.GetsubcategorybyCategoryId;
    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        this.getsubcategoryist = response.response.ref1;
        this.CDR.detectChanges();
      }
    });
  }

  // =========================
  // IMAGE UPLOAD
  // =========================
  async someMethod(event: any) {
    this.imageFileval = await this.gettingFile(event);
  }

  async gettingFile(imagefile: any): Promise<any[]> {
    if (!imagefile) return [];
    const compressed = await this.compressFile(imagefile);
    return [compressed];
  }

  async compressFile(file: any) {
    return await firstValueFrom(
      this.COMPRESSIMAGESERVICE.compress(file).pipe(take(1))
    );
  }

  // =========================
  // ADD PRODUCT
  // =========================
  addbtn(productForm: NgForm) {

    productForm.form.markAllAsTouched();

    // Basic Angular form validation
    if (!productForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill all required fields');
      return;
    }

    // Image conditional validation
    if (this.isImageRequired() && !this.imageFileval?.length) {
      this.notificationService.showMessage(
        'warn',
        'Image Required',
        'Please upload an image when Product Group Code is provided'
      );
      return;
    }

    const formData = new FormData();

    formData.append('categoryid',     String(this.category));
    this.subcategory.forEach((id: number) => formData.append('subcategoryid', id.toString()));
    formData.append('packageid',      String(this.packageid ?? ''));
    formData.append('userid',         String(this.usr_id ?? 0));
    formData.append('producthead',    this.prod_headname);
    formData.append('productcode',    this.prod_code);
    formData.append('productname',    this.prod_name);
    formData.append('productdesc',    this.prod_desc);
    formData.append('productord',     this.prod_ord);
    formData.append('productprice',   String(this.prod_price));
    formData.append('productgrpcod',  this.prod_grpcod);

    // Image: send file if provided, else send empty string (DB allows it when no group code)
    if (this.imageFileval?.length) {
      this.imagejsonvalue = this.imageFileval[0];
      this.photoinfo      = this.imageFileval[0].name;
      formData.append('productimage',     this.imagejsonvalue);
      formData.append('productimagename', this.photoinfo);
    } else {
      formData.append('productimage',     '');
      formData.append('productimagename', '');
    }

    formData.append('username', String(this.username ?? ''));
    formData.append('status',   this.is_active ? '1' : '0');

    const url = GlobalConstants.Authurl + GlobalConstants.Addproduct;

    this.dataService.addData(url, formData).subscribe((response: any) => {
      if (response.status === 'success') {
        this.close();
        this.notificationService.showMessage('success', 'Product Added', 'Product saved successfully');
      } else {
        this.notificationService.showMessage('error', 'Error', 'Failed to save product');
      }
    });
  }

  // =========================
  // CLOSE / CLEAR
  // =========================
  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  clear() {
    this.category      = null;
    this.subcategory   = [];
    this.prod_headname = '';
    this.prod_name     = '';
    this.prod_desc     = '';
    this.prod_ord      = '';
    this.prod_price    = null;
    this.prod_grpcod   = '';
    this.is_active     = '1';
    this.imageFile     = null;
    this.imagePreview  = null;
    this.imageFileval  = [];
  }
}