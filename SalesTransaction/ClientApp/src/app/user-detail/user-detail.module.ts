import { UserDetailComponent } from './user-detail.component';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserDetailService } from './user-detail.service';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

const routes: Routes = [
    {
        path: '',
        component: UserDetailComponent
    }
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        MatTableModule,
        MatButtonModule,
        HttpClientModule
    ],
    declarations: [
        UserDetailComponent
    ],
    providers: [
        UserDetailService
    ],
    exports: [
        UserDetailComponent
    ]
})
export class UserDetailModule {
}

