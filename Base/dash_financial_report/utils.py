import dash_html_components as html
import dash_core_components as dcc


def Header(app):
    return html.Div([get_header(app), html.Br([]), get_menu(app.url_base_pathname)])


def get_header(app):
    header = html.Div(
        [
            html.Div(
                [
                    html.Img(
                        src=app.get_asset_url("dash-financial-logo.png"),
                        className="logo",
                    ),
                    html.A(
                        html.Button("Learn More", id="learn-more-button"),
                        href="https://plot.ly/dash/pricing/",
                    ),
                ],
                className="row",
            ),
            html.Div(
                [
                    html.Div(
                        [html.H5("Calibre Financial Index Fund Investor Shares")],
                        className="seven columns main-title",
                    ),
                    html.Div(
                        [
                            dcc.Link(
                                "Full View",
                                href= app.url_base_pathname + "/full-view",
                                className="full-view-link",
                            )
                        ],
                        className="five columns",
                    ),
                ],
                className="twelve columns",
                style={"padding-left": "0"},
            ),
        ],
        className="row",
    )
    return header


def get_menu(basePath):
    menu = html.Div(
        [
            dcc.Link(
                "Overview",
                href= basePath + "overview",
                className="tab first",
            ),
            dcc.Link(
                "Price Performance",
                href= basePath + "price-performance",
                className="tab",
            ),
            dcc.Link(
                "Portfolio & Management",
                href= basePath + "portfolio-management",
                className="tab",
            ),
            dcc.Link(
                "Fees & Minimums", 
                href= basePath + "fees", 
                className="tab"
            ),
            dcc.Link(
                "Distributions",
                href= basePath + "distributions",
                className="tab",
            ),
            dcc.Link(
                "News & Reviews",
                href= basePath + "news-and-reviews",
                className="tab",
            ),
        ],
        className="row all-tabs",
    )
    return menu


def make_dash_table(df):
    """ Return a dash definition of an HTML table for a Pandas dataframe """
    table = []
    for index, row in df.iterrows():
        html_row = []
        for i in range(len(row)):
            html_row.append(html.Td([row[i]]))
        table.append(html.Tr(html_row))
    return table
