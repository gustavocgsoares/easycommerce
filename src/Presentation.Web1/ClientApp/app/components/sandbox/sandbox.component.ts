import { Component } from '@angular/core';

@Component({
    selector: 'sandbox',
    templateUrl: './sandbox.component.html'
})
export class SandboxComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}
