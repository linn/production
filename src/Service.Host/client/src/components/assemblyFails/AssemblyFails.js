import React from 'react';
import Grid from '@material-ui/core/Grid';
import { ErrorCard, Typeahead } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function AssemblyFails({ items, fetchItems, loading, clearSearch, history }) {
    const forecastItems = items.map(item => ({
        ...item,
        name: item.id,
        description: item.description
    }));

    return (
        <Page>
            <Grid container spacing={3}>
                {/* {error.statusText && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )} */}
                <Grid item xs={12}>
                    <Typeahead
                        items={forecastItems}
                        fetchItems={fetchItems}
                        clearSearch={clearSearch}
                        loading={loading}
                        title="Search"
                        history={history}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFails.propTypes = {
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
    errorMessage: PropTypes.string,
    history: PropTypes.shape({}).isRequired
};

AssemblyFails.defaultProps = {
    loading: false,
    errorMessage: ''
};

export default AssemblyFails;
