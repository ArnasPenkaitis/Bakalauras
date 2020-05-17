import {
    trigger,
    transition,
    style,
    query,
    group,
    animateChild,
    animate,
    keyframes,
} from '@angular/animations';

export const fader = trigger('routeAnimations', [
    transition('* <=> *', [
        query(':enter, :leave', [
            style({
                position: 'absolute',
                left: 0,
                width: '100%',
                opacity: 0,
                transform: 'scale(0) translateY(100%)',
            }),
        ], { optional: true }),
        query(':enter', [
            animate('600ms ease',
                style({
                    opacity: 1,
                    transform: 'scale(1) translateY(0)'
                })
            ),
        ], { optional: true }),
    ]),
]);

export const slider =
    trigger('routeAnimations', [
        transition('* => isLeft', slideTo('left')),
        transition('* => isRight', slideTo('right')),
        transition('isRight => isLeft', slideTo('left')),
        transition('isLeft => isRight', slideTo('right')),
        transition('isRight => *', slideTo('left')),
        transition('isLeft => *', slideTo('right')),
    ]);




function slideTo(direction) {
    const optional = { optional: true };
    return [
        query(':enter, :leave', [
            style({
                position: 'absolute',
                overflow: 'hidden',
                [direction]: 0
            })
        ], optional),
        query(':enter', [
            style({ [direction]: '-100%' })
        ], optional),
        query(':leave', [
            style({ [direction]: '0%' })
        ], optional),
        group([
            query(':leave', [
                animate('800ms ease', style({ [direction]: '100%' })),
            ], optional),
            query(':enter', [
                animate('800ms ease', style({ [direction]: '0%' })),
            ], optional)
        ]),
        style({})
    ];
}
