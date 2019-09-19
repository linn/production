import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ViewManufacturingRoutes from '../../components/manufacturingRoutes/ManufacturingRoutes';
import manufacturingRoutesActions from '../../actions/manufacturingRoutesActions';
import manufacturingRoutesSelectors from '../../selectors/manufacturingRoutesSelectors';

const mapStateToProps = state => ({
    items: manufacturingRoutesSelectors.getItems(state),
    loading: manufacturingRoutesSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = () => () => {
    // dispatch(manufacturingRoutesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    fetchItems: manufacturingRoutesActions.fetchByQueryString
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewManufacturingRoutes));
