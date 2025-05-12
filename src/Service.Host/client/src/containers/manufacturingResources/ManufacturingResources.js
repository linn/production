import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ViewManufacturingResources from '../../components/manufacturingResources/ManufacturingResources';
import manufacturingResourcesActions from '../../actions/manufacturingResourcesActions';
import manufacturingResourcesSelectors from '../../selectors/manufacturingResourcesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: manufacturingResourcesSelectors.getItems(state),
    loading: manufacturingResourcesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.manufacturingResources.item)
});

const initialise = () => dispatch => {
    dispatch(manufacturingResourcesActions.searchWithOptions('', '&includeInvalid=false'));
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewManufacturingResources));
