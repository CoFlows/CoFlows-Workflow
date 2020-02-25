# -*- coding: utf-8 -*-
import dash
import dash_core_components as dcc
import dash_html_components as html
from dash.dependencies import Input, Output
from dash_financial_report.pages import (
    overview,
    pricePerformance,
    portfolioManagement,
    feesMins,
    distributions,
    newsReviews,
)

import pandas as pd
import plotly.graph_objs as go

import requests
from flask import request
import time
import threading

import logging

dash_init = True

def run(port, path):

    global dash_init
    if dash_init:
        dash_init = False
        def inner():

            log = logging.getLogger('werkzeug')
            log.setLevel(logging.ERROR)

            # shutdown existing dash
            try:
                requests.get(url = 'http://localhost:' + str(port) + path + 'shutdown')
                time.sleep(5)
                print('done waiting...')
            except:
                pass

            app = dash.Dash(
               __name__, 
               meta_tags=[{"name": "viewport", "content": "width=device-width"}], 
               url_base_pathname = path,
               assets_folder='/app/mnt/Files/dash_financial_report/assets'
            )

            app.url_base_pathname = path

            app.layout = html.Div(
                [dcc.Location(id="url", refresh=False), html.Div(id="page-content")]
            )


            @app.callback(Output("page-content", "children"), [Input("url", "pathname")])
            def display_page(pathname):
                if pathname == path + "price-performance":
                    return pricePerformance.create_layout(app)
                elif pathname == path + "portfolio-management":
                    return portfolioManagement.create_layout(app)
                elif pathname == path + "fees":
                    return feesMins.create_layout(app)
                elif pathname == path + "distributions":
                    return distributions.create_layout(app)
                elif pathname == path + "news-and-reviews":
                    return newsReviews.create_layout(app)
                elif pathname == path + "full-view":
                    return (
                        overview.create_layout(app),
                        pricePerformance.create_layout(app),
                        portfolioManagement.create_layout(app),
                        feesMins.create_layout(app),
                        distributions.create_layout(app),
                        newsReviews.create_layout(app),
                    )
                else:
                    return overview.create_layout(app)


            # necessary to shutdown server incase the code change
            @app.server.route(path + 'shutdown', methods=['GET'])
            def shutdown():
                print('DASH stopping')
                func = request.environ.get('werkzeug.server.shutdown')
                if func is None:
                    raise RuntimeError('Not running with the Werkzeug Server')
                func()

            return app.run_server(port=port, debug=False, threaded=True)

        server = threading.Thread(target = inner)
        server.start()
        

