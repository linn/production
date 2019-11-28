import { connect } from 'react-redux';
import { TypeaheadDialog } from '@linn-it/linn-form-components-library';
import worksOrdersActions from '../actions/worksOrdersActions';
import worksOrdersSelectors from '../selectors/worksOrdersSelectors';

const mapStateToProps = (state, { onSelect, title }) => ({
    title,
    onSelect,
    searchItems: worksOrdersSelectors
        .getSearchItems(state)
        .map(w => ({ ...w, id: w.orderNumber, name: w.orderNumber })),
    loading: worksOrdersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: worksOrdersActions.search,
    clearSearch: worksOrdersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(TypeaheadDialog);
