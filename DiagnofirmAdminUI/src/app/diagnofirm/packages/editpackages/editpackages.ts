import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';
import { Imageview } from '@/diagnofirm/common/imageview/imageview';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { ConfigService } from '@/diagnofirm/services/config.service';
import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { HttpService } from '@/layout/service/http.service';

import { CommonModule } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

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

// ─── Test Parameters Builder Types ───────────────────────────────────────────

export interface TestParamCard {
  icon: string;
  title: string;
  subTests: string[];
  newSubTest: string;    // transient – used by the input field only
  subTestError: string;  // transient – inline validation message
}

export interface TestParamListItem {
  value: string;
}

// ─────────────────────────────────────────────────────────────────────────────

@Component({
  selector: 'app-editpackages',
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
  templateUrl: './editpackages.html',
  styleUrl: './editpackages.scss'
})
export class Editpackages {

  // =========================================
  // INPUT / OUTPUT
  // =========================================
  @Input() editdisplaypackages: boolean = false;
  @Input() packagedata: any;

  @Output() editdisplayChange: EventEmitter<any> = new EventEmitter<any>();

  // =========================================
  // IMAGE VIEW
  // =========================================
  public visibleImageView = false;
  images: any;

  // =========================================
  // PACKAGE FIELDS
  // =========================================
  packageid: any;

  packg_headname: string = '';
  packg_code: string = '';
  packg_name: string = '';
  packg_samptyp: string = '';
  packg_gender: string = '';
  packg_price: number | null = null;

  packg_testpram: string = '';
  packg_ord: string = '';
  packg_desc: string = '';
  packg_facts: string = '';

  is_active: boolean = true;

  userid: string = '';

  // =========================================
  // IMAGE
  // =========================================
  Imageinfo: any;
  imagename: any;

  imageFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;

  imageFileval!: any[];

  testdata: any;
  visible!: boolean;

  deleteenable: boolean = true;

  imagejsonvalue: any;
  photoinfo: any;

  // =========================================
  // TEST PARAMETERS BUILDER
  // =========================================

  /** Active format: 'card' | 'list' */
  testParamFormat: 'card' | 'list' = 'card';

  /** Card-format data */
  testParamCards: TestParamCard[] = [];

  /** List-format data */
  testParamList: TestParamListItem[] = [];

  /**
   * Called when the radio button selection changes.
   * Clears the data for the format that was NOT selected so
   * only the active format's data is saved to the DB.
   */
  onFormatChange(format: 'card' | 'list') {
    if (format === 'card') {
      this.testParamList = [];
    } else {
      this.testParamCards = [];
    }
  }

//   onFormatChange(format: 'card' | 'list') {

//   // switching TO card
//   if (format === 'card') {

//     // clear list ONLY if card already has data
//     const hasCardData =
//       this.testParamCards.length > 0 &&
//       this.testParamCards.some(c =>
//         c.title?.trim() ||
//         c.subTests?.length > 0
//       );

//     if (hasCardData) {
//       this.testParamList = [];
//     }

//   }

//   // switching TO list
//   else {

//     // clear card ONLY if list already has data
//     const hasListData =
//       this.testParamList.length > 0 &&
//       this.testParamList.some(i =>
//         i.value?.trim()
//       );

//     if (hasListData) {
//       this.testParamCards = [];
//     }
//   }

//   this.testParamFormat = format;
// }

  // ── Card format helpers ──────────────────────────────────────────────────

  addTestParamCard() {
    this.testParamCards.push({ icon: '', title: '', subTests: [], newSubTest: '', subTestError: '' });
  }

  removeTestParamCard(index: number) {
    this.testParamCards.splice(index, 1);
  }

  addSubTest(cardIndex: number) {
    const card = this.testParamCards[cardIndex];
    const val = (card.newSubTest || '').trim();

    if (!val) {
      card.subTestError = 'Sub-test name cannot be empty.';
      return;
    }

    const isDuplicate = card.subTests.some(
      s => s.toLowerCase() === val.toLowerCase()
    );
    if (isDuplicate) {
      card.subTestError = `"${val}" is already added to this category.`;
      return;
    }

    card.subTests.push(val);
    card.newSubTest = '';
    card.subTestError = '';
  }

  /** Clear the inline error as soon as the user types again */
  clearSubTestError(cardIndex: number) {
    this.testParamCards[cardIndex].subTestError = '';
  }

  removeSubTest(cardIndex: number, subIndex: number) {
    this.testParamCards[cardIndex].subTests.splice(subIndex, 1);
  }

  // ── List format helpers ──────────────────────────────────────────────────

  addTestParamListItem() {
    this.testParamList.push({ value: '' });
  }

  removeTestParamListItem(index: number) {
    this.testParamList.splice(index, 1);
  }

  // ── Serialise builder data → packg_testpram string ──────────────────────

  serializeTestParams(): string {
    if (this.testParamFormat === 'card') {
      const payload = this.testParamCards.map(c => ({
        icon: c.icon,
        title: c.title,
        subTests: c.subTests
      }));
      return JSON.stringify({ format: 'card', cards: payload });
    } else {
      const payload = this.testParamList.map(i => i.value).filter(v => v.trim());
      return JSON.stringify({ format: 'list', list: payload });
    }
  }

  /**
   * Parses a stored JSON string (packg_testpram) back into the builder UI state.
   * Falls back gracefully if the value is plain text (legacy data).
   */
  deserializeTestParams(raw: string) {
    if (!raw) return;

    try {
      const parsed = JSON.parse(raw);

      if (parsed.format === 'card' && Array.isArray(parsed.cards)) {
        this.testParamFormat = 'card';
        this.testParamCards = parsed.cards.map((c: any) => ({
          icon: c.icon ?? '',
          title: c.title ?? '',
          subTests: c.subTests ?? [],
          newSubTest: '',
          subTestError: ''
        }));
        return;
      }

      if (parsed.format === 'list' && Array.isArray(parsed.list)) {
        this.testParamFormat = 'list';
        this.testParamList = parsed.list.map((v: string) => ({ value: v }));
        return;
      }
    } catch {
      // Legacy plain-text: treat each line as a list item
      this.testParamFormat = 'list';
      this.testParamList = raw
        .split('\n')
        .filter(l => l.trim())
        .map(l => ({ value: l.trim() }));
    }
  }

  /**
   * Validates that the builder has at least one meaningful entry.
   * For card format also checks that every card has a title and
   * at least one sub-test.
   */
  isTestParamValid(): boolean {
    if (this.testParamFormat === 'card') {
      if (this.testParamCards.length === 0) return false;
      return this.testParamCards.every(
        c => c.title.trim() !== '' && c.subTests.length > 0
      );
    }
    return this.testParamList.some(i => i.value.trim() !== '');
  }

  /**
   * Returns a human-readable summary of card-format errors
   * used in the submit notification.
   */
  getCardValidationErrors(): string[] {
    const errors: string[] = [];
    this.testParamCards.forEach((c, i) => {
      if (!c.title.trim()) {
        errors.push(`Category ${i + 1}: title is required.`);
      }
      if (c.subTests.length === 0) {
        errors.push(`Category ${i + 1} ("${c.title || 'Untitled'}"): add at least one sub-test.`);
      }
    });
    return errors;
  }

  // =========================================
  // OPTIONS
  // =========================================
  genderOptions = [
    { label: 'Male', value: 'Male' },
    { label: 'Female', value: 'Female' },
    { label: 'Both', value: 'Both' }
  ];

  // =========================================
  // CONSTRUCTOR
  // =========================================
  constructor(
    private dataService: DataService,
    private HTTPSERVICE: HttpService,
    private CDR: ChangeDetectorRef,
    private CONFIGSERVICE: ConfigService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService,
    private sanitizer: DomSanitizer
  ) { }

  // =========================================
  // INIT
  // =========================================
  ngOnInit() {
    this.testdata = true;
    this.packageid = this.packagedata?.id;
    this.getpackagebyId();
    this.Imageinfo = this.packagedata?.packageimage;
    this.imagename = this.packagedata?.packageimagename;
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.packageid = this.packagedata?.id;
    this.getpackagebyId();
    this.CDR.detectChanges();
  }

  // =========================================
  // CLOSE
  // =========================================
  close() {
    this.editdisplaypackages = false;
    this.editdisplayChange.emit(this.editdisplaypackages);
  }

  // =========================================
  // DELETE IMAGE
  // =========================================
  deleteenablebtn() {
    this.Imageinfo = null;
    this.deleteenable = false;
  }

  onImageRemove() {
    this.images = null;
    this.imagename = null;
    this.deleteenable = false;
  }

  // =========================================
  // HTML TO TEXT
  // =========================================
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
          if (['h1', 'h2', 'h3', 'p', 'div', 'section', 'article'].includes(tag)) output += '\n';
          if (tag === 'li') output += '\n• ';
          if (tag === 'br') output += '\n';
          walk(child);
          if (['h1', 'h2', 'h3', 'p', 'li', 'div'].includes(tag)) output += '\n';
        }
      });
    };

    walk(container);
    return output.replace(/\n\s+\n/g, '\n').replace(/\n{3,}/g, '\n\n').trim();
  }

  // =========================================
  // GET PACKAGE BY ID
  // =========================================
  getpackagebyId() {
    const input = { packageid: Number(this.packageid) };
    let url = GlobalConstants.Authurl + GlobalConstants.GetpackagebyId;

    this.dataService.addData(url, input).subscribe((res: any) => {
      if (res.status == 'success') {
        const data = res.response.ref1[0];

        this.packg_headname = data.packagehead;
        this.packg_code = data.packagecode;
        this.packg_name = data.packagename;
        this.packg_samptyp = data.packagesampletype;
        this.packg_gender = data.packagegender;
        this.packg_price = data.packageprice;
        this.packg_ord = data.packageord;
        this.packg_desc = data.packagedesc;
        this.packg_facts = data.packagefacts;
        this.is_active = data.is_active === '1';

        // Populate builder from stored JSON
        this.deserializeTestParams(data.packagetestparam ?? '');

        // Image
        this.deleteenable = true;
        this.images = {
          imagenamevalue: data.packageimagename,
          imageBase64value: 'data:image/jpeg;base64,' + data.packageimage
        };
      }
    });
  }

  // =========================================
  // VIEW IMAGE
  // =========================================
  getImagemasterbyId(rowid: any) {
    const input = { Packageid: rowid, username: '1' };
    let url = GlobalConstants.Authurl + GlobalConstants.GetpackageByIdviewImage;

    this.HTTPSERVICE.post(url, input).subscribe((response: any) => {
      if (response.status == 'success') {
        this.images = [];
        const img = response['response'][0];
        this.visibleImageView = true;
        this.visible = false;
        this.images = {
          imagenamevalue: img.imagename,
          imageBase64value: this.sanitizer.bypassSecurityTrustUrl(
            'data:image/jpeg;base64,' + img.imagedata
          )
        };
        this.visibleImageView = true;
        this.CDR.detectChanges();
      }
    });
  }

  imageview() {
    this.visibleImageView = true;
    this.visible = false;
    this.getImagemasterbyId(this.packageid);
  }

  // =========================================
  // IMAGE COMPRESS
  // =========================================
  async someMethod(event: any) {
    this.imageFileval = await this.gettingFile(event);
  }

  async gettingFile(imagefile: any) {
    const conImgFileArray: any[] = [];
    if (!imagefile) return [];
    const compressed = await this.compressFile(imagefile);
    conImgFileArray.push(compressed);
    return conImgFileArray;
  }

  async compressFile(file: any) {
    const result = await firstValueFrom(
      this.COMPRESSIMAGESERVICE.compress(file).pipe(take(1))
    );
    return result;
  }

  // =========================================
  // UPDATE PACKAGE
  // =========================================
  editbtn(packageForm: NgForm) {

    if (!packageForm.valid || !this.isTestParamValid()) {
      const cardErrors = this.getCardValidationErrors();
      const detail = cardErrors.length
        ? cardErrors[0]
        : 'Fill all required fields';
      this.notificationService.showMessage('error', 'Missing Fields', detail);
      return;
    }

    // Serialise builder → JSON string
    this.packg_testpram = this.serializeTestParams();

    const formData = new FormData();

    formData.append('packageid', String(this.packageid));
    formData.append('packagehead', this.packg_headname);
    formData.append('packagecode', this.packg_code);
    formData.append('packagename', this.packg_name);
    formData.append('packagesampletype', this.packg_samptyp);
    formData.append('packagegender', this.packg_gender);
    formData.append('packageprice', String(this.packg_price));
    formData.append('packagetestparam', this.packg_testpram);
    formData.append('packageord', this.packg_ord);
    formData.append('packagedesc', this.packg_desc);
    formData.append('packagefacts', this.packg_facts);

    // IMAGE
    if (this.deleteenable === true) {
      this.imagejsonvalue = this.Imageinfo;
      this.photoinfo = this.imagename;
    } else {
      this.imagejsonvalue = this.imageFileval[0];
      this.photoinfo = this.imageFileval[0].name;
    }

    formData.append('packageimage', this.imagejsonvalue);
    formData.append('packageimagename', this.photoinfo);
    formData.append('username', this.userid);
    formData.append('status', this.is_active ? '1' : '0');

    let url = GlobalConstants.Authurl + GlobalConstants.Updatepackage;

    this.dataService.addData(url, formData).subscribe((res: any) => {
      if (res.status == 'success') {
        this.close();
        this.notificationService.showMessage('success', 'Package Updated', 'Package updated successfully');
      } else {
        this.notificationService.showMessage('error', 'Error', 'Failed to update package');
      }
    });
  }

  // =========================================
  // CLEAR
  // =========================================
  clear() {
    this.getpackagebyId(); // reloads all fields including builder
    this.imageFile = null;
    this.imagePreview = null;
  }
}