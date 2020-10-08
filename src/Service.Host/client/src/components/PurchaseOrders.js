import React from 'react';
import Grid from '@material-ui/core/Grid';
import { Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../containers/Page';

function PurchaseOrders({ items, fetchItems, loading, clearSearch, history }) {
    const forecastItems = items.map(item => ({
        ...item,
        name: item.orderNumber,
        description: item.addressee
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typeahead
                        items={forecastItems.map(i => ({ ...i, name: i.name.toString() }))}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Search By Order Number"
                        placeholder="Order Number"
                        history={history}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PurchaseOrders.propTypes = {
    items: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            name: PropTypes.string,
            description: PropTypes.string,
            href: PropTypes.string
        })
    ).isRequired,
    loading: PropTypes.bool,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    history: PropTypes.shape({}).isRequired
};

PurchaseOrders.defaultProps = {
    loading: false
};

export default PurchaseOrders;
