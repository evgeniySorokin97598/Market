import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal, NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { CommentEntity } from 'src/app/Entities/Product';
import { BaseService } from 'src/app/Services/BaseService';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})



export class CommentComponent implements OnInit {
  @Input() comment : CommentEntity  = new CommentEntity();
 
  @Output() onSuggest: EventEmitter<any> = new EventEmitter();
  
  constructor(config: NgbRatingConfig,private _service: BaseService,
    ) { 
      config.readonly = false;
    }

  ngOnInit(): void {
    
  }
  
}
