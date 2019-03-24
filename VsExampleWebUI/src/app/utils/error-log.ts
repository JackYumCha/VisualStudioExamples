export interface IException{
    ClassName: string;
    Message: string;
    Source: string,
    StackTraceString: string;
    InnerException: IException;
}

export module ErrorLog {

    export function log(exception: IException){
        let exceptions: IException[] = [exception];
        while(exception.InnerException){
            exception = exception.InnerException;
            exceptions.push(exception);
        }
        exceptions.reverse().forEach(
            ex =>{
                let className = ex.ClassName;
                let message = ex.Message;
                let source = ex.Source;
                let stackTraceString:string = <string> ex.StackTraceString;
                if(className && source && stackTraceString){
                    console.error('Api Error:', ex);
                    console.log(`%cClass Name:`, 'color: grey; padding: 0px 0px;');
                    console.log(`%c${className}`, 'color: blue; padding: 0px 20px;');
                    console.log(`%cSource:`, 'color: grey; padding: 0px 0px;');
                    console.log(`%c${source}`, 'color: blue; padding: 0px 20px;');
                    console.log(`%cMessage:`, 'color: grey; padding: 0px 0px;');
                    console.log(`%c${message}`, 'color: red; padding: 0px 20px;');
                    console.log(`%cStack Trace:`, 'color: grey; padding: 0px 0px;');
                    stackTraceString.split('\n').forEach(line => {
                        line = line.replace(/^\s+/ig,'');
                        console.log(`%c${line}`, 'color: brown; padding: 0px 20px;');
                    });
                }
            }
        )
    }
}