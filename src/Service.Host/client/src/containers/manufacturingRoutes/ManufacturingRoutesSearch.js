import { connect } from 'react-redux';
import { TypeaheadDialog } from '@linn-it/linn-form-components-library';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';

const mapStateToProps = (state, { onSelect, title }) => ({
    title,
    onSelect,
    searchItems: manufacturingRoutesSelectors
        .getSearchItems(state)
        .map(w => ({ ...w, id: w.orderNumber, name: w.orderNumber })),
    loading: manufacturingRoutesSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: manufacturingRoutesActions.search,
    clearSearch: manufacturingRoutesActions.clearSearch
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(TypeaheadDialog);
