import { Directive, ElementRef, HostListener, Output, EventEmitter, Input } from '@angular/core';

@Directive({
    selector: '[appClickOutside]'
})
export class ClickOutsideDirective {
    constructor(private _elementRef: ElementRef) {
    }

    @Input() ignoreElementSelectors: string[] = [];

    @Output() clickOutside: EventEmitter<any> = new EventEmitter();

    @HostListener('document:click', ['$event.target'])
    public onMouseEnter(targetElement: any) {
        if (document.body.contains(targetElement)) {
            const clickedInside = this._elementRef.nativeElement.contains(targetElement);

            for(let i = 0; i < this.ignoreElementSelectors.length; i++) {
                if (targetElement.closest(this.ignoreElementSelectors[i])) {
                    return;
                }
            }

            if (!clickedInside) {
                this.clickOutside.emit(null);
            }
        }
    }
}
