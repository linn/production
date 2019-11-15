import { connect } from 'react-redux';
import { getItemError } from '@linn-it/linn-form-components-library';
import ViewManufacturingRoutes from '../../components/manufacturingRoutes/ManufacturingRoutes';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: manufacturingRoutesSelectors.getItems(state),
    loading: manufacturingRoutesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.manufacturingRoutes.item)
});

const mapDispatchToProps = {
    fetchItems: manufacturingRoutesActions.fetchByQueryString
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ViewManufacturingRoutes);
