import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <h1>Lintulajien bongaus!</h1>
            <p></p>
            <p>Toiminnot:</p>
            <ul>
                <li><strong>Ohje</strong>Tämä sivu</li>
                <li><strong>Laskuri</strong>. Lukumäärien päivitys</li>
                <li><strong>Raportti</strong>.Raportti bongauksista.</li>
                <li><strong>Lisää lintu</strong>. Uuden linnun lisäys.</li>
            </ul>
            
        </div>;
    }
}
