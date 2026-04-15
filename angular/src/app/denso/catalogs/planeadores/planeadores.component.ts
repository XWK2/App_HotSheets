import { Component, OnInit } from '@angular/core';
import { CatalogServiceProxy, PlaneadorDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-planeadores',
  templateUrl: './planeadores.component.html',
  styleUrls: ['./planeadores.component.css']
})
export class PlaneadoresComponent implements OnInit {

  planeadores: PlaneadorDto[] = [];

  planeador: PlaneadorDto = new PlaneadorDto({
    nombre: '',
    correos: []
  });

  nuevoCorreo: string = '';

  constructor(private _catalogService: CatalogServiceProxy) {}

  ngOnInit(): void {
    this.cargarPlaneadores();
  }

  // 🔥 CARGA INICIAL + SELECCIÓN
  cargarPlaneadores(nombreSeleccionado?: string) {
    this._catalogService.getPlaneadores()
      .subscribe(data => {

        this.planeadores = [...data];

        if (this.planeadores.length === 0) {
          this.limpiar();
          return;
        }

        let seleccionado = this.planeadores.find(x => x.nombre === nombreSeleccionado);

        if (!seleccionado) {
          seleccionado = this.planeadores[0];
        }

        this.seleccionarPlaneador(seleccionado.nombre || '');
      });
  }

  // 🔥 SELECCIONAR PLANEADOR
  seleccionarPlaneador(nombre: string) {

    if (!nombre) {
      this.limpiar();
      return;
    }

    const p = this.planeadores.find(x => x.nombre === nombre);

    if (p) {
      this.planeador = new PlaneadorDto({
        nombre: p.nombre,
        correos: [...(p.correos || [])]
      });
    }
  }

  // 🔥 VALIDAR EMAIL
  esCorreoValido(correo: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(correo);
  }

  // 🔥 AGREGAR CORREO
  agregarCorreo() {

    if (!this.nuevoCorreo || this.nuevoCorreo.trim() === '') {
      abp.notify.warn('Ingrese un correo válido');
      return;
    }

    const correo = this.nuevoCorreo.trim();

    if (!this.esCorreoValido(correo)) {
      abp.notify.warn('Formato de correo inválido');
      return;
    }

    if (this.planeador.correos?.includes(correo)) {
      abp.notify.warn('El correo ya existe');
      return;
    }

    this.planeador.correos?.push(correo);

    this.nuevoCorreo = '';
  }

  // 🔥 ELIMINAR CORREO
  eliminarCorreo(correo: string) {
    this.planeador.correos =
      this.planeador.correos?.filter(c => c !== correo);
  }

  // 🔥 GUARDAR CON VALIDACIONES
  guardar() {

    // Validar nombre
    if (!this.planeador.nombre || this.planeador.nombre.trim() === '') {
      abp.notify.warn('Debe ingresar un nombre de planeador');
      return;
    }

    // Validar lista de correos
    if (!this.planeador.correos || this.planeador.correos.length === 0) {
      abp.notify.warn('Debe agregar al menos un correo');
      return;
    }

    // Validar correos no vacíos
    const correosValidos = this.planeador.correos.filter(c => c && c.trim() !== '');

    if (correosValidos.length === 0) {
      abp.notify.warn('Los correos no pueden estar vacíos');
      return;
    }

    const nombreActual = this.planeador.nombre;

    this._catalogService
      .guardarPlaneador(this.planeador.toJSON())
      .subscribe(() => {

        // 🔥 Recargar lista y mantener selección
        this.cargarPlaneadores(nombreActual);

        abp.notify.success('Guardado correctamente');
      });
  }

  // 🔥 LIMPIAR FORM
  limpiar() {
    this.planeador = new PlaneadorDto({
      nombre: '',
      correos: []
    });

    this.nuevoCorreo = '';
  }
}