import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from '@app/home/home.component';
import { NgxAnimatedCounterModule } from '@bugsplat/ngx-animated-counter';
import {
    DxDataGridModule,
    DxPieChartModule,
    DxSelectBoxModule,
    DxDateBoxModule,
    DxButtonModule,
    DxDateRangeBoxModule,
    DxChartModule,
} from 'devextreme-angular';

// layout
import { HeaderComponent } from './layout/header/header.component';
import { HeaderLeftNavbarComponent } from './layout/header/header-left-navbar.component';
import { HeaderLanguageMenuComponent } from './layout/header/header-language-menu.component';
import { HeaderUserMenuComponent } from './layout/header/header-user-menu.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { SidebarLogoComponent } from './layout/sidebar/sidebar-logo.component';
import { SidebarUserPanelComponent } from './layout/sidebar/sidebar-user-panel.component';
import { SidebarMenuComponent } from './layout/sidebar/sidebar-menu.component';

// DevExpress
import { DevExpressModule } from './dev-express/dev-express.module';
import { DensoSharedModule } from './denso/shared/denso-shared.module';

// Dashboard components
import { DashboardCountersComponent } from '@app/home/dashboard-counters.component';
import { CounterDetailsDialogComponent } from '@app/home/counter-details-dialog.component';
import { ChartsDialogComponent } from '@app/home/charts-dialog.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        DashboardCountersComponent,
        CounterDetailsDialogComponent,
        ChartsDialogComponent,

        // layout
        HeaderComponent,
        HeaderLeftNavbarComponent,
        HeaderLanguageMenuComponent,
        HeaderUserMenuComponent,
        FooterComponent,
        SidebarComponent,
        SidebarLogoComponent,
        SidebarUserPanelComponent,
        SidebarMenuComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        ModalModule.forChild(),
        BsDropdownModule,
        CollapseModule,
        TabsModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule,
        DevExpressModule,
        DensoSharedModule,
        NgxAnimatedCounterModule,
        DxDataGridModule,
        DxPieChartModule,
        DxSelectBoxModule,
        DxDateBoxModule,
        DxButtonModule,
        DxDateRangeBoxModule,
        DxChartModule,
    ],
    providers: [],
})
export class AppModule {}
