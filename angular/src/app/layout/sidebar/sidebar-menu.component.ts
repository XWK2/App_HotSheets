import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { Router, RouterEvent, NavigationEnd, PRIMARY_OUTLET } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { MenuItem } from '@shared/layout/menu-item';
import { SettingsParametersDto, TenantSettingsEditDto } from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'sidebar-menu',
    templateUrl: './sidebar-menu.component.html',
})
export class SidebarMenuComponent extends AppComponentBase implements OnInit {
    menuItems: MenuItem[];
    menuItem: MenuItem;
    menuItemsMap: { [key: number]: MenuItem } = {};
    activatedMenuItems: MenuItem[] = [];
    routerEvents: BehaviorSubject<RouterEvent> = new BehaviorSubject(undefined);
    homeRoute = '/app/home';
    settings: TenantSettingsEditDto;

    settingsParameters: SettingsParametersDto[] = [];
    constructor(injector: Injector, private router: Router) {
        super(injector);
        this.router.events.subscribe(this.routerEvents);
    }

    ngOnInit(): void {
        this.menuItems = this.getMenuItems();
        this.patchMenuItems(this.menuItems);
        this.routerEvents.pipe(filter((event) => event instanceof NavigationEnd)).subscribe((event) => {
            const currentUrl = event.url !== '/' ? event.url : this.homeRoute;
            const primaryUrlSegmentGroup = this.router.parseUrl(currentUrl).root.children[PRIMARY_OUTLET];
            if (primaryUrlSegmentGroup) {
                this.activateMenuItems('/' + primaryUrlSegmentGroup.toString());
            }
        });
    }

    getMenuItems(): MenuItem[] {
        return [
            new MenuItem(this.l('HomePage'), '/app/home', 'fas fa-home'),
            new MenuItem(this.l('ShippingInstructions'), '/app/shipping/instructions', 'fas fa-truck', 'Pages.ShippingInstructions'),
            new MenuItem(
                this.l('PendingForApproval'),
                '/app/shipping/instructions/pending-for-approval',
                'fa-solid fa-list-check',
                'Pages.ShippingInstructions.PendingForApproval'
            ),
            new MenuItem(this.l('Templates'), '/app/shipping/instructions/templates', 'fa-solid fa-file-invoice', 'Pages.ShippingInstructions.Templates'),
            
            // new MenuItem(this.l('Surveys'), '/app/surveys', 'fa-solid fa-square-poll-vertical', 'Pages.Surveys'),
            
            new MenuItem(this.l('StarSheets'), '/app/starSheets', 'fa-solid fa-square-poll-vertical', 'Pages.StarSheets'),

            new MenuItem(this.l('PurchaseOrders'), '/app/purchaseOrders', 'fa-solid fa-square-poll-vertical', 'Pages.PurchaseOrders'),

            new MenuItem(this.l('HotSheets'), '/app/hotSheets', 'fa-solid fa-square-poll-vertical', 'Pages.HotSheets'),
            
            new MenuItem(this.l('Reports'), '', 'fas fa-chart-bar', 'Pages.Reports', [
                new MenuItem(this.l('HotSheetsReports'), '/app/reports/report-hot-sheets', 'fas fa-receipt', 'Pages.Reports.HotSheetsReports'),                                                
                new MenuItem(this.l('PurchaseOrdersReports'), '/app/reports/report-purchase-orders', 'fas fa-receipt', 'Pages.Reports.PurchaseOrdersReports'),                                                
            ]),
            new MenuItem(this.l('Catalogs'), '', 'fas fa-database', 'Pages.Catalogs', [
                new MenuItem(this.l('Employees'), '/app/catalogs/employees', 'fas fa-users', 'Pages.Catalogs.Employees'),
                new MenuItem(this.l('Plants'), '/app/catalogs/plants', 'fas fa-laptop-house', 'Pages.Catalogs.Plants'),
                new MenuItem(this.l('Departments'), '/app/catalogs/departments', 'fas fa-building-user', 'Pages.Catalogs.Departments'),
                new MenuItem(this.l('Carriers'), '/app/catalogs/carriers', 'fas fa-truck-fast', 'Pages.Catalogs.Carriers'),
                new MenuItem(this.l('DocumentTypes'), '/app/catalogs/document-types', 'fas fa-file-invoice', 'Pages.Catalogs.DocumentTypes'),
                //new MenuItem(this.l('PartNumbers'), '/app/catalogs/part-numbers', 'fas fa-parking', 'Pages.Catalogs.PartNumbers'),
                // new MenuItem(
                //     this.l('PartNumberPricesMenu'),
                //     '/app/catalogs/part-number-prices',
                //     'fa-solid fa-hand-holding-dollar',
                //     'Pages.Catalogs.PartNumberPrices'
                // ),
                //new MenuItem(this.l('ProductCodesSat'), '/app/catalogs/product-codes-sat', 'fas fa-clipboard-list', 'Pages.Catalogs.ProductCodesSAT'),
                
                new MenuItem(this.l('TransportMode'), '/app/catalogs/transport-mode', 'fas fa-clipboard-list', 'Pages.Catalogs.TransportMode'),
                new MenuItem(this.l('StatusHotSheet'), '/app/catalogs/status-hot-sheet', 'fas fa-clipboard-list', 'Pages.Catalogs.StatusHotSheet'),
                new MenuItem(this.l('ShortageShift'), '/app/catalogs/shortage-shift', 'fas fa-clipboard-list', 'Pages.Catalogs.ShortageShift'),

                //new MenuItem(this.l('UnitMeasures'), '/app/catalogs/unit-measures', 'fas fa-ruler-combined', 'Pages.Catalogs.UnitMeasures'),
                //new MenuItem(this.l('ShipmentTerms'), '/app/catalogs/shipment-terms', 'fas fa-handshake', 'Pages.Catalogs.ShipmentTerms'),
                //new MenuItem(this.l('ShipmentReasons'), '/app/catalogs/shipment-reasons', 'fas fa-tags', 'Pages.Catalogs.ShipmentReasons'),
                //new MenuItem(this.l('PaidBy'), '/app/catalogs/paid-by', 'fas fa-user-tie', 'Pages.Catalogs.PaidBy'),
                //new MenuItem(this.l('PaymentTerms'), '/app/catalogs/payment-terms', 'fa-solid fa-file-invoice-dollar', 'Pages.Catalogs.PaymentTerms'),
                //new MenuItem(this.l('RmaAssigned'), '/app/catalogs/rma-assignment', 'fas fa-id-card-alt', 'Pages.Catalogs.RMAAssignments'),
                //new MenuItem(this.l('Customers'), '/app/catalogs/customers', 'fa-solid fa-users-gear', 'Pages.Catalogs.Customers'),
                //new MenuItem(this.l('Services'), '/app/catalogs/services', 'fa-solid fa-truck-plane', 'Pages.Catalogs.Services'),
                //new MenuItem(this.l('Staff'), '/app/catalogs/staff', 'fa-solid fa-user-check', 'Pages.Catalogs.IEStaff'),
                new MenuItem(this.l('HelpInfo'), '/app/catalogs/help-info', 'fa-solid fa-circle-info', 'Pages.Catalogs.HelpInfo'),
                //new MenuItem(this.l('Currencies'), '/app/catalogs/currencies', 'fa-solid fa-money-check-dollar', 'Pages.Catalogs.Currencies'),
                new MenuItem(this.l('LogServices'), '/app/catalogs/log-services', 'fa-solid fa-bars-progress', 'Pages.Catalogs.LogServices'),

                //new MenuItem(this.l('PartNumbersInternalMenu'), '/app/catalogs/part-numbers-internal', 'fas fa-parking', 'Pages.Catalogs.PartNumbersInternal'),
                // new MenuItem(
                //     this.l('PartNumberPricesInternalMenu'),
                //     '/app/catalogs/part-number-prices-internal',
                //     'fa-solid fa-hand-holding-dollar',
                //     'Pages.Catalogs.PartNumberPricesInternal'
                // ),
                //new MenuItem(this.l('Notices'), '/app/catalogs/notices', 'fa-solid fa-circle-info', 'Pages.Catalogs.Notices'),
            ]),
            new MenuItem(this.l('Administration'), '', 'fas fa-tools', 'Pages.Administration', [
                new MenuItem(this.l('Roles'), '/app/admin/roles', 'fas fa-theater-masks', 'Pages.Administration.Roles'),
                new MenuItem(this.l('Users'), '/app/admin/users', 'fas fa-users', 'Pages.Administration.Users'),
                // new MenuItem(this.l('AuditLogs'), '/app/admin/audit-logs', 'fas fa-cog', 'Pages.Administration.AuditLogs'),
                new MenuItem(this.l('Settings'), '/app/admin/settings', 'fas fa-cog', 'Pages.Administration.Settings'),
            ]),
            new MenuItem(this.l('Help'), `${AppConsts.helpDocumentUrl}`, 'fas fa-question-circle'),
            // new MenuItem(this.l('DevExpress'), '/app/dev-express', 'fas fa-list-alt'),
        ];
    }

    patchMenuItems(items: MenuItem[], parentId?: number): void {
        items.forEach((item: MenuItem, index: number) => {
            item.id = parentId ? Number(parentId + '' + (index + 1)) : index + 1;
            if (parentId) {
                item.parentId = parentId;
            }
            if (parentId || item.children) {
                this.menuItemsMap[item.id] = item;
            }
            if (item.children) {
                this.patchMenuItems(item.children, item.id);
            }
        });
    }

    activateMenuItems(url: string): void {
        this.deactivateMenuItems(this.menuItems);
        this.activatedMenuItems = [];
        const foundedItems = this.findMenuItemsByUrl(url, this.menuItems);
        foundedItems.forEach((item) => {
            this.activateMenuItem(item);
        });
    }

    deactivateMenuItems(items: MenuItem[]): void {
        items.forEach((item: MenuItem) => {
            item.isActive = false;
            item.isCollapsed = true;
            if (item.children) {
                this.deactivateMenuItems(item.children);
            }
        });
    }

    findMenuItemsByUrl(url: string, items: MenuItem[], foundedItems: MenuItem[] = []): MenuItem[] {
        items.forEach((item: MenuItem) => {
            if (item.route === url) {
                foundedItems.push(item);
            } else if (item.children) {
                this.findMenuItemsByUrl(url, item.children, foundedItems);
            }
        });
        return foundedItems;
    }

    activateMenuItem(item: MenuItem): void {
        item.isActive = true;
        if (item.children) {
            item.isCollapsed = false;
        }
        this.activatedMenuItems.push(item);
        if (item.parentId) {
            this.activateMenuItem(this.menuItemsMap[item.parentId]);
        }
    }

    isMenuItemVisible(item: MenuItem): boolean {
        if (!item.permissionName) {
            return true;
        }
        return this.permission.isGranted(item.permissionName);
    }
}
