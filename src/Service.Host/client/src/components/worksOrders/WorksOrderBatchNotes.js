import React from 'react';
import Grid from '@material-ui/core/Grid';
import { TypeaheadTable } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function WorksOrdersBatchNotes({ items, fetchItems, loading, history }) {
    const formatDate = date => `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
    const columnNames = ['Works Order', 'Batch Number', 'Part', 'Qty', 'Qty Built', 'Date Raised'];

    const table = {
        totalItemCount: items.length,
        rows: items.map((item, i) => ({
            id: `${item.orderNumber}`,
            values: [
                { id: `${i}-0`, value: `${item.orderNumber}` },
                { id: `${i}-1`, value: `${item.batchNumber || ''}` },
                { id: `${i}-2`, value: item.partNumber },
                { id: `${i}-3`, value: `${item.quantity}` },
                { id: `${i}-4`, value: item.quantityBuilt?.toString() },
                { id: `${i}-5`, value: formatDate(new Date(item.dateRaised)) }
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
                        clearSearch={() => {}}
                        loading={loading}
                        title="Works Order Batch Notes"
                        history={history}
                        placeholder="Part Number..."
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
    history: PropTypes.shape({}).isRequired
};

WorksOrdersBatchNotes.defaultProps = {
    loading: false,
    items: []
};

export default WorksOrdersBatchNotes;
