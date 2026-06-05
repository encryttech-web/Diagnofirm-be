import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { Imageview } from '@/diagnofirm/common/imageview/imageview';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { SelectModule } from 'primeng/select';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
import { firstValueFrom, take } from 'rxjs';

@Component({
  selector: 'app-editproduct',
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
    ToggleSwitchModule,
    Imageupload,
    Imageview,
    EditorModule
  ],
  templateUrl: './editproduct.html',
  styleUrl: './editproduct.scss'
})
export class Editproduct {

  // =========================
  // INPUT / OUTPUT
  // =========================
  @Input() editdisplayproduct: boolean = false;
  @Input() productdata: any;
  @Input() rowvalue: any;
  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();

  // =========================
  // IMAGE VIEW
  // =========================
  visibleImageView = false;
  images: any;
  deleteenable: boolean = true;
  imagejsonvalue: any;
  photoinfo: any;
  Imageinfo: any;
  imagename: any;
  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  imageFileval!: any[];
  testdata: any;
  visible!: boolean;

  // =========================
  // MASTER DATA
  // =========================
  getcategoryist: any;
  getsubcategoryist: any;

  // =========================
  // PRODUCT FIELDS
  // =========================
  productId: any;
  category: any = null;
  subcategory: any = [];
  prod_headname: string = '';
  prod_code: string = '';
  prod_name: string = '';
  prod_desc: string = '';
  prod_ord: string = '';
  prod_price: number | null = null;
  prod_grpcod: string = '';
  is_active: boolean = true;
  userid: string = '';

  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService,
    private sanitizer: DomSanitizer
  ) {}

  // =========================
  // LIFECYCLE
  // =========================
  ngOnInit() {
    this.testdata = true;
    this.getcategory();
    this.getsubcategory(0);
    this.productId  = this.productdata?.id;
    this.Imageinfo  = this.productdata?.['productimage'];
    this.imagename  = this.productdata?.['imagenamevalue'];
    this.getproductbyId();
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.productId = this.productdata?.id;
    this.getproductbyId();
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
  // IMAGE ACTIONS
  // =========================
  deleteenablebtn() {
    this.Imageinfo    = null;
    this.deleteenable = false;
  }

  onImageRemove() {
    this.images       = null;
    this.imagename    = null;
    this.deleteenable = false;
  }

  imageviwe() {
    this.visibleImageView = true;
    this.images = null;
    this.getImagemasterbyId(this.productId);
  }

  // =========================
  // CATEGORY
  // =========================
  getcategory() {
    const url = GlobalConstants.Authurl + GlobalConstants.Getcategory;
    this.dataService.getData(url).subscribe((res: any) => {
      if (res.status === 'success') {
        this.getcategoryist = res.response.ref1;
      }
    });
  }

  // =========================
  // SUBCATEGORY
  // =========================
  getsubcategory(selectedId?: number) {
    const url = GlobalConstants.Authurl + GlobalConstants.Getsubcategory;
    this.dataService.getData(url).subscribe((res: any) => {
      if (res.status === 'success') {
        this.getsubcategoryist = res.response.ref1;
        if (selectedId) {
          this.subcategory = Number(selectedId);
        }
      }
    });
  }

  // =========================
  // GET PRODUCT BY ID
  // =========================
  getproductbyId() {
    if (!this.productId) return;

    const input = { productid: Number(this.productId) };
    const url   = GlobalConstants.Authurl + GlobalConstants.GetproductbyId;

    this.dataService.addData(url, input).subscribe((res: any) => {
      if (res.status === 'success') {
        const data = res.response.ref1[0];

        this.category      = data.categoryid;
        this.getsubcategory(data.subcategoryid);
        this.prod_headname = data.productheadname;
        this.prod_code     = data.productcode;
        this.prod_name     = data.productname;
        this.prod_desc     = data.productdescription;
        this.prod_price    = data.productprice;
        this.prod_grpcod   = data.productgroup ?? '';
        this.is_active     = data.is_active === '1';
        this.deleteenable  = true;

        this.images = {
          imagenamevalue:  data.imagenamevalue,
          imageBase64value: 'data:image/jpeg;base64,' + data.productimage
        };

        this.CDR.detectChanges();
      }
    });
  }

  // =========================
  // IMAGE MASTER
  // =========================
  getImagemasterbyId(rowid: any) {
    const input = { Productid: rowid, username: '1' };
    const url   = GlobalConstants.Authurl + GlobalConstants.GetByIdviewImage;

    this.HTTPSERVICE.post(url, input).subscribe((response: any) => {
      if (response.status === 'success') {
        const img = response['response'][0];
        this.images = {
          imagenamevalue:  img.imagename,
          imageBase64value: this.sanitizer.bypassSecurityTrustUrl(img.imageBase64value)
        };
        this.visibleImageView = true;
        this.CDR.detectChanges();
      }
    });
  }

  // =========================
  // IMAGE UPLOAD
  // =========================
  onImageSelect(event: any) {
    if (event.files?.length > 0) {
      this.imageFile = event.files[0];
      const reader = new FileReader();
      reader.onload = () => { this.imagePreview = reader.result; };
      if (this.imageFile) reader.readAsDataURL(this.imageFile);
    }
  }

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
  // UPDATE PRODUCT
  // =========================
  editbtn(productForm: NgForm) {

    productForm.form.markAllAsTouched();

    if (!productForm.valid) {
      this.notificationService.showMessage('error', 'Missing Fields', 'Please fill all required fields');
      return;
    }

    // Image conditional validation:
    // Required if group code is filled AND no existing image AND no new image uploaded
    if (this.isImageRequired() && !this.deleteenable && !this.imageFileval?.length) {
      this.notificationService.showMessage(
        'warn',
        'Image Required',
        'Please upload an image when Product Group Code is provided'
      );
      return;
    }

    const formData = new FormData();

    formData.append('productid',      String(this.productId));
    formData.append('categoryid',     String(this.category));
    formData.append('subcategoryid',  String(this.subcategory));
    formData.append('userid',         String(this.userid));
    formData.append('producthead',    this.prod_headname);
    formData.append('productcode',    this.prod_code);
    formData.append('productname',    this.prod_name);
    formData.append('productdesc',    this.prod_desc);
    formData.append('productord',     this.prod_ord);
    formData.append('productprice',   String(this.prod_price));
    formData.append('productgrpcod',  this.prod_grpcod);

    // Use existing image if still present, else use newly uploaded file, else send empty
    if (this.deleteenable) {
      // existing image kept
      formData.append('productimage',     this.Imageinfo ?? '');
      formData.append('productimagename', this.imagename ?? '');
    } else if (this.imageFileval?.length) {
      // new image uploaded
      formData.append('productimage',     this.imageFileval[0]);
      formData.append('productimagename', this.imageFileval[0].name);
    } else {
      // no image (allowed only when no group code)
      formData.append('productimage',     '');
      formData.append('productimagename', '');
    }

    formData.append('username', this.userid);
    formData.append('status',   this.is_active ? '1' : '0');

    const url = GlobalConstants.Authurl + GlobalConstants.Updateproduct;

    this.dataService.addData(url, formData).subscribe((res: any) => {
      if (res.status === 'success') {
        this.close();
        this.notificationService.showMessage('success', 'Product Updated', 'Product updated successfully');
      } else {
        this.notificationService.showMessage('error', 'Error', 'Failed to update product');
      }
    });
  }

  // =========================
  // CLOSE / CLEAR
  // =========================
  close() {
    this.editdisplayproduct = false;
    this.editdisplayChange.emit(this.editdisplayproduct);
  }

  clear() {
    this.getproductbyId();  // restore original values
    this.imageFile    = null;
    this.imagePreview = null;
    this.imageFileval = [];
  }

  // =========================
  // UTIL
  // =========================
  htmlToPlainText(html: string): string {
    const container = document.createElement('div');
    container.innerHTML = html;
    let output = '';
    const walk = (node: any) => {
      node.childNodes.forEach((child: any) => {
        if (child.nodeType === 3) {
          const text = child.textContent.trim();
          if (text) output += text + ' ';
        }
        if (child.nodeType === 1) {
          const tag = child.tagName.toLowerCase();
          if (['h1','h2','h3','p','div','section','article'].includes(tag)) output += '\n';
          if (tag === 'li') output += '\n• ';
          if (tag === 'br') output += '\n';
          walk(child);
          if (['h1','h2','h3','p','li','div'].includes(tag)) output += '\n';
        }
      });
    };
    walk(container);
    return output.replace(/\n\s+\n/g, '\n').replace(/\n{3,}/g, '\n\n').trim();
  }
}