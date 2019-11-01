import React from 'react';
import Grid from '@material-ui/core/Grid';
import { TypeaheadTable } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function WorksOrderLabels({ items, fetchItems, loading, clearSearch, history }) {
    const columnNames = ['Part Number', 'Sequence', 'Label Text'];

    const table = {
        totalItemCount: items.length,
        rows: items.map((item, i) => ({
            id: `${item.orderNumber}`,
            values: [
                { id: `${i}-0`, value: `${item.partNumber}` },
                { id: `${i}-1`, value: item.sequence },
                { id: `${i}-2`, value: `${item.labelText}` }
            ],
            links: item.links
        }))
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TypeaheadTable
                        table={table}
                        columnNames={columnNames}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Works Order Labels"
                        history={history}
                        placeholder="Part Number..."
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

WorksOrderLabels.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ),
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

WorksOrderLabels.defaultProps = {
    loading: false,
    items: []
};

export default WorksOrderLabels;
