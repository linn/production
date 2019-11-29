import { connect } from 'react-redux';
import { getItemError } from '@linn-it/linn-form-components-library';
import ViewManufacturingRoutes from '../../components/productionTriggerLevels/TriggerLevels';
import productionTriggerLevelsActions from '../../actions/productionTriggerLevelsActions';
import productionTriggerLevelsSelectors from '../../selectors/productionTriggerLevelsSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: productionTriggerLevelsSelectors.getItems(state),
    loading: productionTriggerLevelsSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.productionTriggerLevels.item)
});

const mapDispatchToProps = {
    fetchItems: productionTriggerLevelsActions.fetchByQueryString
};

export default connect(mapStateToProps, mapDispatchToProps)(ViewManufacturingRoutes);
