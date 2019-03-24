console.log('Hello world');


function wait(milliseconds: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
        setTimeout(() => resolve(), milliseconds);
    });
}

function myDecorator<T extends { new(...args: any[]): {} }>(constructor: T) {
    console.log('log decorator');
    return class extends constructor {
        newProperty = "new property";
        hello = "override";
    }
}

function myDecoratorBuilder() {
    console.log('log decorator builder');
    return myDecorator;
}

console.log('log before class defintion');
@myDecoratorBuilder()
class TestClass {

    async test() {
        console.log(0);
        await wait(50);
        console.log(1);
        await wait(100);
        console.log(2);
        await wait(200);
        console.log(3);
    }
}

