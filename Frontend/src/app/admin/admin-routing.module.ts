import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLoginGuard } from '../_Guards/admin-login.guard';
import { AdminHomeComponent } from './admin-home/admin-home.component';

const routes: Routes =[
    {path:"",component:AdminHomeComponent,canActivate:[AdminLoginGuard],children:[
        {path:"",component:AdminHomeComponent},
        // {path:"info",component:DoctorInfoComponent},
        // {path:"edit",component:DoctorEditComponent},
        // {path:"patientsearch",component:DoctorPatientSearchComponent},
        // {path:"patient/:id",component:DoctorPatientComponent,children:[
        //     {path:"info",component:DoctorPatientInfoComponent},
        //     {path:"history",component:PatientHistoryComponent},
        // ]},
        // {path:"recordpre/:pid/:date",component:RecordPrescriptionComponent},
        // {path:"editpre/:id/:date",component:EditPrescriptionComponent}
    ]},
    {path:"home",component:AdminHomeComponent,canActivate:[AdminLoginGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }