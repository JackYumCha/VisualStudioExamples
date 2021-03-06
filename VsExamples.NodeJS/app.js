var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
console.log('Hello world');
function wait(milliseconds) {
    return new Promise((resolve, reject) => {
        setTimeout(() => resolve(), milliseconds);
    });
}
function myDecorator(constructor) {
    console.log('log decorator');
    return class extends constructor {
        constructor() {
            super(...arguments);
            this.newProperty = "new property";
            this.hello = "override";
        }
    };
}
function myDecoratorBuilder() {
    console.log('log decorator builder');
    return myDecorator;
}
console.log('log before class defintion');
let TestClass = class TestClass {
    test() {
        return __awaiter(this, void 0, void 0, function* () {
            console.log(0);
            yield wait(50);
            console.log(1);
            yield wait(100);
            console.log(2);
            yield wait(200);
            console.log(3);
        });
    }
};
TestClass = __decorate([
    myDecoratorBuilder()
], TestClass);
//# sourceMappingURL=app.js.map