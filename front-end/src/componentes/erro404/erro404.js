import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../../App.css';


class agendamento extends Component {

    constructor(props) {
        super(props);
        this.state = {
        };
    }

    render() {
        return (
            <div style={{ 'background-image': 'url("https://images.wallpaperscraft.com/image/milky_way_starry_sky_galaxy_119519_1280x720.jpg")', 'background-position': 'center', 'display': 'flex', 'justify-content': 'center' }}>
                <p style={{ 'margin': '0', 'color': 'white', 'padding-top': '47vh', 'padding-bottom': '47vh', 'font-size': '25px' }}>404 - Página não encontrada :(</p>
            </div>
        )
    }
}

export default withRouter(agendamento);
