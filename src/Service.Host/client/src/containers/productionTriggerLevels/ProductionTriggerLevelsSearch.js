import { connect } from 'react-redux';
import { TypeaheadDialog } from '@linn-it/linn-form-components-library';
import productionTriggerLevelsActions from '../../actions/productionTriggerLevelsActions';
import productionTriggerLevelsSelectors from '../../selectors/productionTriggerLevelsSelectors';

const mapStateToProps = (state, { onSelect, title }) => ({
    title,
    onSelect,
    searchItems: productionTriggerLevelsSelectors
        .getSearchItems(state)
        .map(w => ({ ...w, id: w.orderNumber, name: w.orderNumber })),
    loading: productionTriggerLevelsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: productionTriggerLevelsActions.search,
    clearSearch: productionTriggerLevelsActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(TypeaheadDialog);
