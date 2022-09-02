import { connect } from 'react-redux';
import { initialiseOnMount } from '@linn-it/linn-form-components-library';
import WorksOrderSearch from '../../components/worksOrders/WorksOrderSearch';
import worksOrdersActions from '../../actions/worksOrdersActions';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';

const mapStateToProps = (state) => ({
    searchItems: worksOrdersSelectors
        .getSearchItems(state)
        .map(w => ({ ...w, id: w.orderNumber, name: w.orderNumber })),
    loading: worksOrdersSelectors.getSearchLoading(state)
});

const initialise = () => () => {};

const mapDispatchToProps = {
    initialise,
    fetchItems: worksOrdersActions.search,
    clearSearch: worksOrdersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(WorksOrderSearch));
