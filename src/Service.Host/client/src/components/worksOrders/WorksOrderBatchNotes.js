import React from 'react';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';
import TypeAheadTable from './TypeAheadTable';

function WorksOrdersBatchNotes({ items, fetchItems, loading, clearSearch, history }) {
    const formatDate = date => `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
    const columnNames = ['Works Order', 'Part', 'Qty', 'Date Raised'];

    const table = {
        totalItemCount: items.length,
        rows: items.map((item, i) => ({
            id: `${item.orderNumber}`,
            values: [
                { id: `${i}-0`, value: `${item.orderNumber}` },
                { id: `${i}-1`, value: item.partNumber },
                { id: `${i}-2`, value: `${item.quantity}` },
                { id: `${i}-4`, value: formatDate(new Date(item.dateRaised)) }
            ],
            links: item.links
        }))
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TypeAheadTable
                        table={table}
                        columnNames={columnNames}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Works Order Batch Notes"
                        history={history}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

WorksOrdersBatchNotes.propTypes = {
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

WorksOrdersBatchNotes.defaultProps = {
    loading: false,
    items: []
};

export default WorksOrdersBatchNotes;