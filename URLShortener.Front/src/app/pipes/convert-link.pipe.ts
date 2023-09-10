import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'makeTinyLink',
    pure: false,
    })

export class FormattedLinkPipe implements PipeTransform {
    transform(link: string): string {
        if(link.length > 231)
            return link.substring(0,50) + '...' + link.substring(link.length - 11, link.length - 1);
        return link;
    }
}