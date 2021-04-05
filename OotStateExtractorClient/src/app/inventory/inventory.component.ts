import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ExtractorService } from '../extractor.service';
import { SaveContext } from '../models/save_context';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  constructor(private readonly extractorService: ExtractorService) { }

  public saveContext$?: Observable<SaveContext>;

  ngOnInit(): void {
    this.saveContext$ = this.extractorService.extract();
  }
}
