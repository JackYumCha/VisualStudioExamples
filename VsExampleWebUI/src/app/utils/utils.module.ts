import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncludesPipe } from './includes.pipe';
import { IsValidDirective } from './is-valid.directive';
import { RequiredPipe } from './required.pipe';
import { RegexMatchPipe } from './regex-match.pipe';
import { ValueRangePipe } from './value-range.pipe';
import { IsEmailPipe } from './is-email.pipe';
import { LoaderCircleComponent } from './loader-circle/loader-circle.component';
import { EnsureFieldPipe } from './ensure-field.pipe';

@NgModule({
  declarations: [
    IncludesPipe,
    IsValidDirective,
    RequiredPipe,
    RegexMatchPipe,
    ValueRangePipe,
    IsEmailPipe,
    LoaderCircleComponent,
    EnsureFieldPipe,
  ],
  imports: [
    CommonModule
  ],
  exports:[
    IncludesPipe,
    IsValidDirective,
    RequiredPipe,
    RegexMatchPipe,
    ValueRangePipe,
    IsEmailPipe,
    LoaderCircleComponent,
    EnsureFieldPipe,
  ]
})
export class UtilsModule { }
