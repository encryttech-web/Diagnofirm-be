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

import { DataService } from '@/diagnofirm/services/data.service';
import { GlobalConstants } from '@/diagnofirm/services/global.constant';
import { NotificationService } from '@/diagnofirm/services/notification.service';
import { CompressImageService } from '@/diagnofirm/services/compress-image.service';
import { firstValueFrom, take } from 'rxjs';
import { Imageupload } from '@/diagnofirm/common/imageupload/imageupload';

// ─── Test Parameters Builder Types ───────────────────────────────────────────

export interface TestParamCard {
  icon: string;
  title: string;
  subTests: string[];
  newSubTest: string;     // transient – used by the input field only
  subTestError: string;  // transient – inline validation message
}

export interface TestParamListItem {
  value: string;
}

// ─────────────────────────────────────────────────────────────────────────────

@Component({
  selector: 'app-addpackages',
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
    EditorModule,
    Imageupload
  ],
  templateUrl: './addpackages.html',
  styleUrl: './addpackages.scss'
})
export class Addpackages {

  @Input() display: boolean = false;
  @Output() displayChange = new EventEmitter<boolean>();
  @Output() dataReloaded = new EventEmitter<any>();

  username!: number;
  usr_id!: number;

  // =========================
  // MASTER DATA
  // =========================
  packageid: any;

  // =========================
  // PACKAGE FIELDS (DB MAPPED)
  // =========================
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

  packg_image: any;
  packg_imgname: string = '';

  is_active: string = '1';
  created_by: string = '';

  // =========================
  // IMAGE HANDLING
  // =========================
  imageFileval: any[] = [];
  imagejsonvalue: any;
  photoinfo: any;

  // =========================
  // TEST PARAMETERS BUILDER
  // =========================

  /** Active tab: 'card' | 'list' */
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
      // Switched to Card → wipe List data
      this.testParamList = [];
    } else {
      // Switched to List → wipe Card data
      this.testParamCards = [];
    }
  }

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

    // ── Validation ───────────────────────────────────────────────────────────
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
    // ── All good ─────────────────────────────────────────────────────────────
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

  // ── Serialise builder data → packg_testpram JSON string ─────────────────
  //
  //  DB column: packagetestparam (TEXT / VARCHAR)
  //
  //  Card format stored as:
  //    { "format": "card", "cards": [ { "icon": "🫀", "title": "Heart", "subTests": ["ECG","Echo"] }, … ] }
  //
  //  List format stored as:
  //    { "format": "list", "list": [ "Full Blood Count (FBC) — ₱106.20", "Lipid Panel — ₱250.00" ] }
  //
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
   * Returns a human-readable summary of card-format errors shown
   * in the submit notification (not used in the template directly).
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

  // =========================
  // OPTIONS
  // =========================
  genderOptions = [
    { label: 'Male', value: 'Male' },
    { label: 'Female', value: 'Female' },
    { label: 'Both', value: 'Both' }
  ];

  statusList = [
    { label: 'Active', value: '1' },
    { label: 'Inactive', value: '0' }
  ];

  Lastcode: any;
  testdata: boolean = true;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private COMPRESSIMAGESERVICE: CompressImageService,
    private CDR: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.testdata = true;
    this.getlastcode('diafrm', 'packages_tbl', 'packg_code');
    this.CDR.detectChanges();
  }

  ngOnChanges() {
    this.getlastcode('diafrm', 'packages_tbl', 'packg_code');
    this.CDR.detectChanges();
  }

  generateNextCode(lastCode: string): string {
    const prefix = 'PAKG';
    if (!lastCode) return `${prefix}-0001`;
    const lastNumber = parseInt(lastCode.split('-')[1], 10);
    const nextNumber = lastNumber + 1;
    const formatted = nextNumber.toString().padStart(4, '0');
    return `${prefix}-${formatted}`;
  }

  getlastcode(schemaname: any, tablename: any, columnname: any) {
    const input = { schemaname, tablename, columnname };
    let url = GlobalConstants.Authurl + GlobalConstants.Getlastcode;

    this.dataService.addData(url, input).subscribe((response: any) => {
      if (response.status == 'success') {
        const value = response?.response?.ref1?.[0]?.ref1 ?? 'PAKG-0000';
        this.Lastcode = value;
        this.packg_code = this.generateNextCode(this.Lastcode);
      }
    });
  }

  // =========================
  // IMAGE COMPRESS
  // =========================
  async someMethod(event: any) {
    this.imageFileval = await this.gettingFile(event);
  }

  async gettingFile(file: any) {
    const arr: any[] = [];
    if (!file) return [];
    const compressed = await this.compressFile(file);
    arr.push(compressed);
    return arr;
  }

  async compressFile(file: any) {
    const res = await firstValueFrom(
      this.COMPRESSIMAGESERVICE.compress(file).pipe(take(1))
    );
    return res;
  }

  // =========================
  // SAVE PACKAGE
  // =========================
  addbtn(form: NgForm) {

    if (!form.valid || !this.isTestParamValid()) {
      const cardErrors = this.getCardValidationErrors();
      const detail = cardErrors.length
        ? cardErrors[0]   // show first actionable error
        : 'Please fill all required fields';
      this.notificationService.showMessage('error', 'Validation Error', detail);
      return;
    }

    // Serialise builder → JSON string for DB storage
    this.packg_testpram = this.serializeTestParams();

    const formData = new FormData();

    formData.append('usr_id', String(this.usr_id ?? 0));
    formData.append('packagehead', this.packg_headname);
    formData.append('packagecode', this.packg_code);
    formData.append('packagename', this.packg_name);
    formData.append('packagesampletype', this.packg_samptyp);
    formData.append('packagegender', this.packg_gender);
    formData.append('packageprice', String(this.packg_price ?? 0));
    formData.append('packagetestparam', this.packg_testpram);   // ← JSON string
    formData.append('packageord', this.packg_ord);
    formData.append('packagedesc', this.packg_desc);
    formData.append('packagefacts', this.packg_facts);

    if (this.imageFileval && this.imageFileval.length > 0) {
      this.imagejsonvalue = this.imageFileval[0];
      this.photoinfo = this.imagejsonvalue.name;
      formData.append('packageimage', this.imagejsonvalue);
      formData.append('packageimagename', this.photoinfo);
    }

    formData.append('username', String(this.username ?? ''));
    formData.append('status', this.is_active ? '1' : '0');

    let url = GlobalConstants.Authurl + GlobalConstants.Addpackage;

    this.dataService.addData(url, formData).subscribe((res: any) => {
      if (res.status === 'success') {
        this.notificationService.showMessage('success', 'Success', 'Package saved successfully');
        this.close();
      } else {
        this.notificationService.showMessage('error', 'Error', 'Failed to save package');
      }
    });
  }

  // =========================
  // CLOSE
  // =========================
  close() {
    this.display = false;
    this.displayChange.emit(this.display);
    this.dataReloaded.emit();
  }

  // =========================
  // CLEAR
  // =========================
  clear() {
    this.packageid = null;
    this.packg_headname = '';
    this.packg_code = '';
    this.packg_name = '';
    this.packg_samptyp = '';
    this.packg_gender = '';
    this.packg_price = null;
    this.packg_testpram = '';
    this.packg_ord = '';
    this.packg_desc = '';
    this.packg_facts = '';
    this.packg_image = null;
    this.packg_imgname = '';
    this.is_active = '1';
    this.created_by = '';
    this.imageFileval = [];
    this.imagejsonvalue = null;
    this.photoinfo = null;

    // Reset builder
    this.testParamFormat = 'card';
    this.testParamCards = [];
    this.testParamList = [];
  }
}